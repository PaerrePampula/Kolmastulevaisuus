using UnityEngine;
[System.Serializable]
public class Flag
{
    public string FlagName;
    public int TimeToHappen;
    public bool uniqueFlag;
    public Flag(string Flag, int flagTimer = 0, bool unique = false)
    {
        FlagName = Flag;
        TimeToHappen = flagTimer;
        uniqueFlag = unique;

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
    }
}

