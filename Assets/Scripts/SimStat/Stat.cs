using UnityEngine;
using System.Collections;

public class Stat
{
    SimStatType simStatType;
    public delegate void StatChange(float value, float changeInValue, SimStatType simStatType);
    public static event StatChange OnStatChange;
    float _statfloat = 0;
    public float StatFloat
    {
        get => _statfloat; set
        {
            _statfloat = Mathf.Clamp(value, 0, 100);

        }
    }
    public Stat(SimStatType type)
    {
        simStatType = type;

    }
    public void Init()
    {
        GameEventSystem.RegisterListener(Event_Type.SIMSTAT_CHANGE, changeStat);
    }
    void changeStat(EventInfo info)
    {
        SimStatInfo simStatInfo = (SimStatInfo)info;
        if (simStatInfo.SimStatName == simStatType)
        {
            ChangeStat(simStatInfo.StatChange);
        }
    }
    public void ChangeStat(float amount)
    {
        StatFloat += amount;
        OnStatChange.Invoke(_statfloat, amount, simStatType);
    }
}
