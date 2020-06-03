public class IncomeSource : IStattable
{
    #region Fields
    float incomeAmountTotalOneMonth;
    Job incomeSourceJob;
    public bool UniqueStat { get { return false; } }
    public StatType ThisStatType { get { return StatType.PlayerIncomeSource; } }
    #endregion
    #region constructors
    public IncomeSource(float NewIncomeAmount, Job sourceOfIncomeJob = null)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
        incomeSourceJob = (sourceOfIncomeJob != null) ? sourceOfIncomeJob : null;
        StatsChecker.RegisterStat(this);

    }
    public IncomeSource(float NewIncomeAmount)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
        StatsChecker.RegisterStat(this);
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
    public T getValue<T>()
    {
        return (T)(object)getGrossIncomeAmountInAMonth();
    }
    #endregion
}
