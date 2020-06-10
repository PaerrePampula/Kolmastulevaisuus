using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerEconomy
{
    #region Fields
    public delegate void IncreaseAction(float amount);
    public static event IncreaseAction OnIncrease;
    //Delegatesta ja eventistä. Näillä kukkaro saa lähetettyä rahanmuutoksesta viestin kaikille onincreasen tilanneille 
    //classeille viestin siitä, että rahatilanne on muuttunut, delegaten ja eventin avulla näitä tilaajia ei tarvitse
    //erikseen unityn editorissä määritellä, eli toisinsanoen pelaajan kukkaron ei tarvitse tietää, kenelle tätä viestiä lähetetään.

    static PlayerEconomy()
    {
        JobHandler.OnJobEnd += deleteCurrentJobIncomeIfCan;
        GameEventSystem.RegisterListener(Event_Type.FLOAT_CHANGE, SetMoney);
        GameEventSystem.RegisterListener(Event_Type.JOB_REGISTERED_TO_PLAYER, RegisterAnIncomeSourceFromJob);

        TaxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));

        DateTimeSystem.OnMonthChange += PayFromIncomeSources;

    }
    public static List<IncomeSource> incomeSources() //Shorthand PlayerDataHolder.IncomeSources
    {
        return PlayerDataHolder.IncomeSources;
    }
    #endregion

    #region Getters and setters
    public static void SetMoney(EventInfo info)
    {
        FloatChangeInfo floatChangeInfo = (FloatChangeInfo)info;
        PlayerDataHolder.PlayerMoney.MoneyChange(floatChangeInfo.changeofFloat);
        OnIncrease?.Invoke(PlayerDataHolder.PlayerMoney.getValue<float>());
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
        TaxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));
    }

    static void PayFromIncomeSources()
    {
        for (int i = 0; i < incomeSources().Count; i++)
        {
            PaerToolBox.changePlayerMoney(incomeSources()[i].getNetIncomeInAMonth());
        }
        Flag flag = GlobalGameFlags.GetFlag("FIRST_INCOME_RECEIVED");
        if (flag == null)
        {
            flag = new Flag("FIRST_INCOME_RECEIVED",0, true);
            GlobalGameFlags.addFlag(flag);
            flag.FireFlag();
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
}
