public interface IWelfareableSupport
{
    float CalculatedSupport();
    bool checkValidityOfSupport();
    typeOfSupport GetTypeOfSupport();
    (System.DateTime, System.DateTime) getStartAndEndDate();
    bool isAMonthlySupport { get; set; }
}
