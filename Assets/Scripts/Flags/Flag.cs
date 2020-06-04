using UnityEngine;
[System.Serializable]
public class Flag
{
    public string FlagName;
    public int TimeToHappen;
    public Flag(string Flag, int flagTimer = 0)
    {
        FlagName = Flag;
        TimeToHappen = flagTimer;


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

