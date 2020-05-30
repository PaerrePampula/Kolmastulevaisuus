using UnityEngine;
using System.Collections;

public class NationalIncomeTaxBracket
{
    #region Fields
    float bracketLowerLimit;
    float bracketHighLimit;
    float bottomLineBaseTax;
    float taxedPercentileAmountForIncomeAboveTheLowerLimit;
    #endregion
    #region Constructors
    public NationalIncomeTaxBracket(float lower, float higher, float bottom, float percentile)
    {
        bracketLowerLimit = lower;
        bracketHighLimit = higher;
        bottomLineBaseTax = bottom;
        taxedPercentileAmountForIncomeAboveTheLowerLimit = percentile;
    }
    #endregion
    #region Getters
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
    #endregion
}
