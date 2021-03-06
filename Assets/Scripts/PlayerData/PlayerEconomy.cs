﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerEconomy
{
    #region Fields

    public delegate void FloatValue(float amount);
    public static event FloatValue OnChangeFetch;
    public delegate void NewIncomeApply();
    public static event NewIncomeApply OnNewIncome;
    public delegate void PaidExpenses();
    public static event PaidExpenses onExpensesPay;

    //Delegatesta ja eventistä. Näillä kukkaro saa lähetettyä rahanmuutoksesta viestin kaikille onincreasen tilanneille 
    //classeille viestin siitä, että rahatilanne on muuttunut, delegaten ja eventin avulla näitä tilaajia ei tarvitse
    //erikseen unityn editorissä määritellä, eli toisinsanoen pelaajan kukkaron ei tarvitse tietää, kenelle tätä viestiä lähetetään.

    static PlayerEconomy()
    {

        GameEventSystem.RegisterListener(Event_Type.FLOAT_CHANGE, SetMoney);
        GameEventSystem.RegisterListener(Event_Type.JOB_REGISTERED_TO_PLAYER, RegisterAnIncomeSourceFromJob);
        GameEventSystem.RegisterListener(Event_Type.EXTRA_INCOME, registerExtraIncome);

        TaxationSystem.getIncomeafterMandatoryPayments(getAllIncomeSourceGrossTotals(12));

        DateTimeSystem.OnMonthChange += PayFromIncomeSources;
        DateTimeSystem.OnMonthChange += deductExpenses;

    }
    public static List<Incomeable> incomeSources() //Shorthand PlayerDataHolder.IncomeSources
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
        OnChangeFetch?.Invoke(floatChangeInfo.changeofFloat);
        PlayerDataHolder.Current.PlayerMoney.MoneyChange(floatChange);

    }
    static void setMoney(float amount)
    {
        float floatChange = 0;
        floatChange = amount;
        PlayerDataHolder.Current.PlayerMoney.MoneyChange(floatChange);
        OnChangeFetch?.Invoke(amount);

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



        incomeSources().Add(job.job);
        TaxationSystem.getIncomeafterMandatoryPayments(getAllIncomeSourceGrossTotals(12));

        OnNewIncome.Invoke();
    }
    static void registerExtraIncome(EventInfo info)
    {
        ExtraIncomeInfo extra = (ExtraIncomeInfo)info;
        IncomeSource incomeSource = new IncomeSource(extra.extraIncomeAmount, true);
        incomeSources().Add(incomeSource);

        TaxationSystem.getIncomeafterMandatoryPayments(getAllIncomeSourceGrossTotals(12));

        OnNewIncome.Invoke();
    }

    static void PayFromIncomeSources()
    {
        for (int i = 0; i < incomeSources().Count; i++)
        {
            if (incomeSources().Count == 0) return;
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
        removeExtras();

    }

    public static float totalSpeculatedNetIncomeInAMonth()
    {
        float net = 0;
        for (int i = 0; i < incomeSources().Count; i++)
        {
            net += incomeSources()[i].getSpeculatedNetIncomeInAMonth();
        }
        return net;
    }

    public static void createPurchase(string name, float amount, bool stacked = false)
    {
        setMoney(amount);
        ListableExpense expense = new ListableExpense(PlayerDataHolder.OtherListableExpenses, (name, amount), stacked);
    }
    //static void checkBelowZeroMoney()
    //{
    //    float checkedAmount = PlayerDataHolder.Current.PlayerMoney.getValue<float>();
    //    float bankBackUp = Bank.Current.SavedMoney.getValue<float>();
    //    if (checkedAmount < 0)
    //    {
    //        float bankCheck = checkedAmount + bankBackUp;
    //        if (bankCheck > 0)
    //        {
    //            Bank.Current.SavedMoney.MoneyChange(checkedAmount);
    //            Flag savingsSave = new Flag("PLAYER_ECONOMY_SAVED_BY_SAVINGS", 0, false);
    //            savingsSave.FireFlag();
    //            PlayerDataHolder.Current.PlayerMoney.resetMoney();
    //            return;
    //        }
    //        int strikesGenerated = (checkedAmount > 500f) ? 2 : 1;
    //        //Pelaaja menettää kaksi "elämää" jos hänen velkansa on yli 500
    //        OnBust?.Invoke(strikesGenerated);
    //    }
    //}
    static void deductExpenses()
    {
        for (int i = 0; i < PlayerDataHolder.MonthlyListableExpenses.Count; i++)
        {
            PaerToolBox.changePlayerMoney(-PlayerDataHolder.MonthlyListableExpenses[i].getTotal());
        }
        onExpensesPay?.Invoke();
    }
    static void removeExtras()
    {
        for (int i = 0; i < incomeSources().Count; i++)
        {
            if (incomeSources()[i].AnExtra == true)
            {
                incomeSources().Remove(incomeSources()[i]);

                OnNewIncome.Invoke();
            }
        }

    }

}
