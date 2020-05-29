using UnityEngine;
using System.Collections;

public class OpintoLaina : BaseTuki
{
    float StudentDebtBaseAmountForStudentsInFinnishAcademicStudies = 650f;
    System.DateTime firstDateToRaiseLoan;
    public OpintoLaina(System.DateTime start, System.DateTime end, bool Monthly) : base(start, end, Monthly)
    {
        firstDateToRaiseLoan = base.dateOfWelfareBegins;
    }
    public override float CalculatedSupport() => StudentDebtBaseAmountForStudentsInFinnishAcademicStudies;

}
