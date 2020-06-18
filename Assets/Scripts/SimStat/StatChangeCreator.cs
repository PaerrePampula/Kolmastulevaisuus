using UnityEngine;
using System.Collections;

public static class StatChangeCreator
{

    [RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.BeforeSceneLoad)] //Ei runaa ilman tätä koska ei monobehaviouria.
    static void OnRuntimeMethodLoad()
    { 
        PlacementHelper.OnObjectSatisfaction += comfortChange;
    }
    static void comfortChange(float change)
    {
        SimStatInfo SimChangeAction = new SimStatInfo();
        SimChangeAction.SimStatName = SimStatType.Comfortableness;
        SimChangeAction.StatChange = change;


        GameEventSystem.DoEvent(
            Event_Type.SIMSTAT_CHANGE,
            SimChangeAction
            );
    }
}
