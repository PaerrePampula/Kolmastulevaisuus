using UnityEngine;
using System.Collections;

public class IncomeSource
{
    float incomeAmountTotalOneMonth;

    public IncomeSource(float NewIncomeAmount)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
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
