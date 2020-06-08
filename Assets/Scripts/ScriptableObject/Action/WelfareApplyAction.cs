using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName ="Actions/WelfareApplyAction")]
public class WelfareApplyAction : ScriptableAction
{
    public typeOfSupport typeofWelfare;
    
    public override void PerformAction()
    {
        WelfareApplyFormInfo form = new WelfareApplyFormInfo();
        form.typeofWelfare = typeofWelfare;
        thisEvent_Type = Event_Type.PLAYER_WANTS_WELFARE;
        GameEventSystem.DoEvent(
            thisEvent_Type,
            form
            );
    }
}
