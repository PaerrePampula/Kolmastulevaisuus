using UnityEngine;
using System.Collections;

public static class Satisfaction
{
    public delegate void SatisfactionChange(float value, float changeInValue);
    public static event SatisfactionChange OnSatisfactionChange;
    static float _satisfaction = 0;
    public static float SatisfactionFloat
    {
        get => _satisfaction; set
        {
            _satisfaction = Mathf.Clamp(value, 0, 100);

        }
    }
    static Satisfaction()
    {
        GameEventSystem.RegisterListener(Event_Type.SIMSTAT_CHANGE, changeStat);
    }
    static void changeStat(EventInfo info)
    {
        SimStatInfo simStatInfo = (SimStatInfo)info;
        if (simStatInfo.SimStatName == "Satisfaction")
        {
            ChangeStat(simStatInfo.StatChange);
        }
    }
    public static void ChangeStat(float amount)
    {
        SatisfactionFloat += amount;
        OnSatisfactionChange.Invoke(_satisfaction, amount);
    }
}
