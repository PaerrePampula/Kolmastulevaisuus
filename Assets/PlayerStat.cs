using UnityEngine;
using System.Collections;

public struct PlayerStat
{
    public string statName;
    public string statValueString;
    public float statValueToFloat()
    {
        return float.Parse(statValueString);
    }
    public bool booleanValue;
}
