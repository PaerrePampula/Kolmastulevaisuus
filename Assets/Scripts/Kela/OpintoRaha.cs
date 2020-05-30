using UnityEngine;
using System.Collections;

public class OpintoRaha : BaseTuki
{
    #region Fields
    float baseSupportAmountInStudentSupportForIndenpendentlyLivingAdultStudents = 252.76f;
    #endregion
    #region constructors
    public OpintoRaha(System.DateTime start, System.DateTime end, bool Monthly, typeOfSupport TypeOfSupport) : base(start, end, Monthly, TypeOfSupport) { }
    #endregion
    public override float CalculatedSupport() //TODO: IÄN VAIKUTUS, AVIOLIITON VAIKUTUS. MAHDOLLISTEN LASTEN VAIKUTUS.
    {
        return baseSupportAmountInStudentSupportForIndenpendentlyLivingAdultStudents;
    }

}
