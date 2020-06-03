public class IncomeSource
{
    #region Fields
    float incomeAmountTotalOneMonth;
    Job incomeSourceJob;
    #endregion
    #region constructors
    public IncomeSource(float NewIncomeAmount, Job sourceOfIncomeJob = null)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
        incomeSourceJob = (sourceOfIncomeJob != null) ? sourceOfIncomeJob : null;

    }
    public IncomeSource(float NewIncomeAmount)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
    }
    #endregion
    #region Getters and setters
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
    #endregion
}
