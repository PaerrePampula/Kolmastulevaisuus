using UnityEngine;
[System.Serializable]
public class Flag
{
    public string FlagName;
    public int TimeToHappen;
    public bool uniqueFlag;
    public float flagOptionalValue;
    public delegate void FlagFire(Flag flag);
    public static event FlagFire OnFlagFire;
    public Flag(string Flag, int flagTimer = 0, bool unique = false, float flagvalue = 0)
    {
        FlagName = Flag;
        TimeToHappen = flagTimer;
        uniqueFlag = unique;
        flagOptionalValue = flagvalue;

    }
    public void FireFlag()
    {
        FlagFireInfo flagInfo = new FlagFireInfo();
        flagInfo.flag = this;
        Debug.Log("FLAG FIRE: " + FlagName);
        GameEventSystem.DoEvent(
            Event_Type.FLAG_FIRE,
            flagInfo
            );
        OnFlagFire.Invoke(this);
    }
}

