using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerEconomy
{
    #region Fields
    public delegate void IncreaseAction(float amount);
    public static event IncreaseAction OnIncrease;
    public delegate void NewIncomeApply();
    public static event NewIncomeApply OnNewIncome;
    public delegate void MoneyAlert(int strikeCost);
    public static event MoneyAlert OnBust;
    //Delegatesta ja eventistä. Näillä kukkaro saa lähetettyä rahanmuutoksesta viestin kaikille onincreasen tilanneille 
    //classeille viestin siitä, että rahatilanne on muuttunut, delegaten ja eventin avulla näitä tilaajia ei tarvitse
    //erikseen unityn editorissä määritellä, eli toisinsanoen pelaajan kukkaron ei tarvitse tietää, kenelle tätä viestiä lähetetään.

    static PlayerEconomy()
    {
        JobHandler.OnJobEnd += deleteCurrentJobIncomeIfCan;
        GameEventSystem.RegisterListener(Event_Type.FLOAT_CHANGE, SetMoney);
        GameEventSystem.RegisterListener(Event_Type.JOB_REGISTERED_TO_PLAYER, RegisterAnIncomeSourceFromJob);

        TaxationSystem.getIncomeafterMandatoryPayments(getAllIncomeSourceGrossTotals(12));

        DateTimeSystem.OnMonthChange += PayFromIncomeSources;
        DateTimeSystem.OnMonthChange += checkBelowZeroMoney;

    }
    public static List<IncomeSource> incomeSources() //Shorthand PlayerDataHolder.IncomeSources
    {
        return PlayerDataHolder.Current.IncomeSources;
    }
    #endregion

    #region Getters and setters
    public static void SetMoney(EventInfo info)
    {
        float floatChange = 0;

        FloatChangeInfo floatChangeInfo = (FloatChangeInfo)info;
        floatChange = floatChangeInfo.changeofFloat;

        PlayerDataHolder.Current.PlayerMoney.MoneyChange(floatChange);
        OnIncrease?.Invoke(PlayerDataHolder.Current.PlayerMoney.getValue<float>());
    }
    static void setMoney(float amount)
    {
        float floatChange = 0;
        floatChange = amount;
        PlayerDataHolder.Current.PlayerMoney.MoneyChange(floatChange);
        OnIncrease?.Invoke(PlayerDataHolder.Current.PlayerMoney.getValue<float>());
    }

    public static float getAllIncomeSourceGrossTotals(int monthAmount)
    {
        float total = 0;
        for (int i = 0; i < incomeSources().Count; i++)
        {
            total += (incomeSources()[i].getGrossIncomeAmountInAMonth() * monthAmount);
        }
        return total;
    }
    #endregion



    static void RegisterAnIncomeSourceFromJob(EventInfo info)
    {
        JobRegisterInfo job = (JobRegisterInfo)info;
        deleteCurrentJobIncomeIfCan(); //Poistetaan mahdollinen entinen työpaikka

        IncomeSource incomeSource = new IncomeSource(job.job.getMonthlyPaymentAmount(), job.job);

        incomeSources().Add(incomeSource);
        TaxationSystem.getIncomeafterMandatoryPayments(getAllIncomeSourceGrossTotals(12));

        OnNewIncome.Invoke();
    }

    static void PayFromIncomeSources()
    {
        for (int i = 0; i < incomeSources().Count; i++)
        {
            if (GlobalGameFlags.GetFlag("PLAYER_HAS_APPLIED_TAXFORM") == null)
            {
                Flag taxFlag = new Flag("PLAYER_INFORMED_ABOUT_TAXFORM", 0, true);
                taxFlag.FireFlag();
                PaerToolBox.changePlayerMoney(incomeSources()[i].getNetIncomeInAMonth() * 0.6f); ; //Pelaaja ei ole hakenut verokorttia, rankaise

            }
            else
            {
                PaerToolBox.changePlayerMoney(incomeSources()[i].getNetIncomeInAMonth());
                Flag flag = GlobalGameFlags.GetFlag("FIRST_INCOME_RECEIVED");
                if (flag == null)
                {
                    flag = new Flag("FIRST_INCOME_RECEIVED", 0, true);
                    GlobalGameFlags.addFlag(flag);
                    flag.FireFlag();
                }
            }

        }


    }
    static void deleteCurrentJobIncomeIfCan()
    {
        var hadJob = incomeSources().SingleOrDefault(income => income.GetJob() != null);
        if (hadJob != null)
        {
            incomeSources().Remove(hadJob);
        }
    }
    public static float totalNetIncomeInAMonth()
    {
        float net = 0;
        for (int i = 0; i < incomeSources().Count; i++)
        {
            net += incomeSources()[i].getNetIncomeInAMonth();
        }
        return net;
    }
    public static void createPurchase(string name, float amount, bool stacked = false)
    {
        setMoney(amount);
        ListableExpense expense = new ListableExpense(PlayerDataHolder.Current.OtherListableExpenses, (name, amount), stacked);
    }
    static void checkBelowZeroMoney()
    {
        float checkedAmount = PlayerDataHolder.Current.PlayerMoney.getValue<float>();
        if (checkedAmount < 0)
        {

            int strikesGenerated = (checkedAmount > 850f) ? 2 : 1;
            //Pelaaja menettää kaksi "elämää" jos hänen velkansa on yli 850
            OnBust?.Invoke(strikesGenerated);
        }
    }

}
