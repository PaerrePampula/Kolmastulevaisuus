using UnityEngine;
using System.Collections;

public class OpintoLaina : BaseTuki
{
    #region Fields
    float StudentDebtBaseAmountForStudentsInFinnishAcademicStudies = 650f;
    System.DateTime firstDateToRaiseLoan;
    #endregion
    #region constructors
    public OpintoLaina(System.DateTime start, System.DateTime end, bool Monthly, typeOfSupport TypeOfSupport) : base(start, end, Monthly, TypeOfSupport) 
    {
        firstDateToRaiseLoan = base.dateOfWelfareBegins;
    }
    #endregion
    public override float CalculatedSupport() => StudentDebtBaseAmountForStudentsInFinnishAcademicStudies;

}
