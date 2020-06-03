using UnityEngine;

public abstract class EventInfo
{
    string eventDebugInformation;
    //Debug.login käyttöön.
}
public class FloatChangeInfo : EventInfo
{
    public float changeofFloat;
}
public class ColorChangeInfo : EventInfo
{
    public Color32 newColor;
}
public class CameraAngleChangeInfo : EventInfo
{
    public int increments;
    public float changeofFloat;
}
public class EventRaise : EventInfo //Event raiseri, käyttöesimerkkinä joku triggerin triggeröinti nostaa uuden eventin eventcontrollista.
{
    public bool SpecificEventRaise; //Nostaako tämä raise jonkun tietyn eventin?
    public RandomEventScriptable InCaseSpecificEvent; //Jos nostaa, täytä tämä siihen.
}
public class JobInfo : EventInfo //Käytettty mm työn haussa 
{
    public JobNotice jobNotice;
    public int indexOnJobSearch;
}
public class JobRegisterInfo : EventInfo
{
    public Job job;
}
public class StatChangeInfo : EventInfo
{
    public IStattable stattable;
}
public class UiElementCall : EventInfo
{
    public GameObject elementToCall;
    public Transform elementParent;
}
public class WelfareApplyFormInfo : EventInfo
{
    public typeOfSupport typeofWelfare;
    public (System.DateTime, System.DateTime) timeWelfareAppliedFor = (DateTimeSystem.getCurrentDate(), new System.DateTime(DateTimeSystem.getCurrentDate().Year, 12, 31));
}
public class RentLeaseForm : EventInfo
{
    public RentableHome rentable;
}
public class FlagFireInfo : EventInfo
{
    public Flag flag;
}