using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Actions/PreReqChange")]
public class DemoPreReqChange : ScriptableAction
{
    public PrereqPair[] preRequisites;

    public override void PerformAction()
    {
        PreRequisiteChange valueChangeAction = new PreRequisiteChange();
        valueChangeAction.prereqs = preRequisites;
        GameEventSystem.Current.DoEvent(
            Event_Type,
            valueChangeAction
            );
    }
}
