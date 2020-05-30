using UnityEngine;
using System.Collections;

public class IncomeSource
{
    float incomeAmountTotalOneMonth;
<<<<<<< HEAD
    Job incomeSourceJob;
    public IncomeSource(float NewIncomeAmount, Job sourceOfIncomeJob = null)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
        incomeSourceJob = (sourceOfIncomeJob != null) ? sourceOfIncomeJob : null;
=======

    public IncomeSource(float NewIncomeAmount)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
>>>>>>> experimental-KELA
    }
    public float getGrossIncomeAmountInAMonth()
    {
        return incomeAmountTotalOneMonth;
    }
    public float getAnnualGrossIncome()
    {
        return 12 * incomeAmountTotalOneMonth;
    }
    public float getNetIncomeInAMonth()
    {
        return incomeAmountTotalOneMonth * TaxationSystem.getPlayerTaxRateInverse();
    }
    public void setIncome(float value)
    {
        incomeAmountTotalOneMonth = value;
    }

}
