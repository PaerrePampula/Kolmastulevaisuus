using UnityEngine;
using System.Collections;

public interface IWelfareableSupport
{
    float CalculatedSupport();
    bool checkValidityOfSupport();
    (System.DateTime, System.DateTime) getStartAndEndDate();
    bool isAMonthlySupport { get; set; }
}
