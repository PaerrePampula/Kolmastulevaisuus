using System.Collections.Generic;
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
        GameEventSystem.RegisterListener(Event_Type.FLOAT_CHANGE, SetMoney);
        GameEventSystem.RegisterListener(Event_Type.JOB_REGISTERED_TO_PLAYER, RegisterAnIncomeSourceFromJob);

        TaxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));

        DateTimeSystem.OnMonthChange += PayFromIncomeSources;

    }
    private static List<IncomeSource> incomeSources() //Shorthand PlayerDataHolder.IncomeSources
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
    static void GetMoney() //Debug. poista joskus
    {
        Debug.Log(PlayerDataHolder.PlayerMoney);
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
        IncomeSource incomeSource = new IncomeSource(job.job.getMonthlyPaymentAmount());

        incomeSources().Add(incomeSource);
        TaxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));
    }

    static void PayFromIncomeSources()
    {
        for (int i = 0; i < incomeSources().Count; i++)
        {
            PaerToolBox.changePlayerMoney(incomeSources()[i].getNetIncomeInAMonth());
        }
    }

}
