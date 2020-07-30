using UnityEngine;
using System.Collections;

public class Stat
{
    SimStatType simStatType;
    public delegate void StatChange(float value, float changeInValue, SimStatType simStatType);
    public static event StatChange OnStatChange;
    float _statfloat = 0;
    float maxValue;
    float minValue;
    public float StatFloat
    {
        get => _statfloat; set
        {
            _statfloat = Mathf.Clamp(value, minValue, maxValue);

        }
    }

    public SimStatType SimStatType { get => simStatType; set => simStatType = value; }

    public Stat(SimStatType type, float min, float max)
    {
        SimStatType = type;
        minValue = min;
        maxValue = max;

    }
    public void Init()
    {
        GameEventSystem.RegisterListener(Event_Type.SIMSTAT_CHANGE, changeStat);
        ResetButton.onReset += unInit;
    }
    void unInit()
    {
        GameEventSystem.UnRegisterListener(Event_Type.SIMSTAT_CHANGE, changeStat);
        ResetButton.onReset -= unInit;
    }
    void changeStat(EventInfo info)
    {
        SimStatInfo simStatInfo = (SimStatInfo)info;
        if (simStatInfo.SimStatName == SimStatType)
        {
            ChangeStat(simStatInfo.StatChange);
        }
    }
    public void ChangeStat(float amount)
    {
        StatFloat += amount;
        OnStatChange.Invoke(_statfloat, amount, SimStatType);

    }
    public void SetStat(float amount)
    {
        StatFloat = amount;
        OnStatChange.Invoke(_statfloat, amount, SimStatType);
    }
}
