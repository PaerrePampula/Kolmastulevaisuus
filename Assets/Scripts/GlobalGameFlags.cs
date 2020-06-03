using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalGameFlags : MonoBehaviour
{
    [SerializeField]
    List<Flag> AllGlobalFlags; //Näitä flageja ois ihan kiva katella inspectorissa joten monobehaviour 
    private void Awake()
    {
        AllGlobalFlags = new List<Flag>();
        GameEventSystem.RegisterListener(Event_Type.FLAG_FIRE, addFlag);
    }
    Flag GetFlag(string flagName)
    {
        var flag = AllGlobalFlags.SingleOrDefault(x => x.FlagName == flagName);
        if (flag != null)
        {
            return flag;
        }
        return null;
    }
    void addFlag(EventInfo info) 
    {
        FlagFireInfo flagFireInfo = (FlagFireInfo)info;
        AllGlobalFlags.Add(flagFireInfo.flag);
    }
}
