using UnityEngine;
using System.Collections;
using System;

public abstract class BaseTuki : IWelfareableSupport
{
    typeOfSupport typeOfWelfare;
    public bool isAMonthlySupport { get { return _monthly; } set { _monthly = value; } }
    protected System.DateTime dateOfWelfareBegins;
    protected System.DateTime dateofWelfareEnds;
    protected bool _monthly = true;
    public abstract float CalculatedSupport();
    public BaseTuki(System.DateTime start, System.DateTime end, bool monthly, typeOfSupport typeOfSupport)
    {
        dateOfWelfareBegins = start;
        dateofWelfareEnds = end;
        _monthly = monthly;
        typeOfWelfare = typeOfSupport;
    }
    public bool checkValidityOfSupport()
    {
        int result = System.DateTime.Compare(dateofWelfareEnds, DateTimeSystem.getCurrentDate());
        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public (DateTime, DateTime) getStartAndEndDate()
    {
        (DateTime, DateTime) dates = (dateOfWelfareBegins, dateofWelfareEnds);
        return dates;
    }
}
