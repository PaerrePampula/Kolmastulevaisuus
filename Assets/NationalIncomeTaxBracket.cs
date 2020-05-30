using UnityEngine;
using System.Collections;

public class NationalIncomeTaxBracket
{
    float bracketLowerLimit;
    float bracketHighLimit;
    float bottomLineBaseTax;
    float taxedPercentileAmountForIncomeAboveTheLowerLimit;
    public NationalIncomeTaxBracket(float lower, float higher, float bottom, float percentile)
    {
        bracketLowerLimit = lower;
        bracketHighLimit = higher;
        bottomLineBaseTax = bottom;
        taxedPercentileAmountForIncomeAboveTheLowerLimit = percentile;
    }
    public float getLower()
    {
        return bracketLowerLimit;
    }
    public float getUpper()
    {
        return bracketHighLimit;
    }
    public float getBottomLineTax()
    {
        return bottomLineBaseTax;
    }
    public float getPercentileTax()
    {
        return taxedPercentileAmountForIncomeAboveTheLowerLimit;
    }
}
