using UnityEngine;
using System.Collections;

public class OpintoRaha : BaseTuki
{
    float baseSupportAmountInStudentSupportForIndenpendentlyLivingAdultStudents = 252.76f;
    public OpintoRaha(System.DateTime start, System.DateTime end, bool Monthly, typeOfSupport TypeOfSupport) : base(start, end, Monthly, TypeOfSupport) { }
    public override float CalculatedSupport() //TODO: IÄN VAIKUTUS, AVIOLIITON VAIKUTUS. MAHDOLLISTEN LASTEN VAIKUTUS.
    {
        return baseSupportAmountInStudentSupportForIndenpendentlyLivingAdultStudents;
    }

}
