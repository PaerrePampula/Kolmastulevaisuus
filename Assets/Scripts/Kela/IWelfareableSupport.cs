using UnityEngine;
using System.Collections;

public interface IWelfareableSupport
{
    float CalculatedSupport();
    bool checkValidityOfSupport();
    bool isAMonthlySupport { get; set; }
}
