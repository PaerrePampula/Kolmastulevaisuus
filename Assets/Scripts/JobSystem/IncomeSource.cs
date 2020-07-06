public class IncomeSource : IStattable, Incomeable
{
    #region Fields
    float incomeAmountTotalOneMonth;
    Job incomeSourceJob;
    bool anExtra = false;
    public bool UniqueStat { get { return false; } }
    public StatType ThisStatType { get { return StatType.PlayerIncomeSource; } }

    public bool AnExtra { get => anExtra; set => anExtra = value; }

    public delegate void IncomeMake();
    public static event IncomeMake OnNewIncomeRegister;
    #endregion
    #region constructors
    public IncomeSource(float NewIncomeAmount, Job sourceOfIncomeJob = null)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
        incomeSourceJob = sourceOfIncomeJob;
        StatsChecker.RegisterStat(this);
        OnNewIncomeRegister?.Invoke();
    }
    public IncomeSource(float NewIncomeAmount, bool extra = false)
    {
        incomeAmountTotalOneMonth = NewIncomeAmount;
        AnExtra = extra;
        StatsChecker.RegisterStat(this);
        OnNewIncomeRegister?.Invoke();
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

        return incomeAmountTotalOneMonth * TaxationSystem.netPaymentPercentage();
    }
    public float getSpeculatedNetIncomeInAMonth()
    {
        return getNetIncomeInAMonth();
    }
    public void setIncome(float value)
    {
        incomeAmountTotalOneMonth = value;
    }
    public T getValue<T>()
    {
        return (T)(object)getGrossIncomeAmountInAMonth();
    }
    public Job GetJob()
    {
        return incomeSourceJob;
    }
    #endregion
}
