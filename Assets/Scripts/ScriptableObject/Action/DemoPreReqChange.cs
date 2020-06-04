using UnityEngine;
[CreateAssetMenu(menuName = "Actions/PreReqChange")]
public class DemoPreReqChange : ScriptableAction
{
    //public PlayerStat stats;

    public override void PerformAction()
    {
        StatChangeInfo valueChangeAction = new StatChangeInfo();
        //valueChangeAction.playerStat = stats;
        GameEventSystem.DoEvent(
            Event_Type,
            valueChangeAction
            );
    }
}
