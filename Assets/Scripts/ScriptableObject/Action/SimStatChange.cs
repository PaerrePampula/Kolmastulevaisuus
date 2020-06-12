using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Actions/SimStatChange")]
public class SimStatChange : ScriptableAction
{
    public float StatChangeAmount;
    public SimStatType simStatType;
    public override void PerformAction()
    {
        SimStatInfo SimChangeAction = new SimStatInfo();
        SimChangeAction.SimStatName = simStatType.ToString();
        SimChangeAction.StatChange = StatChangeAmount;
        thisEvent_Type = Event_Type.SIMSTAT_CHANGE;

        GameEventSystem.DoEvent(
            thisEvent_Type,
            SimChangeAction
            );
    }
}

