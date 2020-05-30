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
        GameEventSystem.Current.DoEvent(
            Event_Type,
            form
            );
    }
}
