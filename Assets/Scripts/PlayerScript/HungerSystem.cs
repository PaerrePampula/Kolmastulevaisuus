using UnityEngine;
using System.Collections;

public class HungerSystem : MonoBehaviour
{

    private void OnEnable()
    {
        LocationHandler.OnLocationChange += incrementHunger;
        GameEventSystem.RegisterListener(Event_Type.SIMSTAT_CHANGE, incrementHunger);

    }
    void incrementHunger()
    {
        PlayerDataHolder.Current.Hunger.ChangeStat(10f);
    }
    void incrementHunger(EventInfo info)
    {
        SimStatInfo simStatInfo = (SimStatInfo)info;
        if(simStatInfo.SimStatName == SimStatType.Hunger)
        {
            PlayerDataHolder.Current.Hunger.ChangeStat(simStatInfo.StatChange);
        }
    }
}
