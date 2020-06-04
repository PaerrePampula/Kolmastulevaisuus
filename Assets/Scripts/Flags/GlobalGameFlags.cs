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
    public Flag GetFlag(string flagName)
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

        if (uniqueFlagIsApplicable(flagFireInfo.flag))
        {
            AllGlobalFlags.Add(flagFireInfo.flag);
        }

    }
    bool uniqueFlagIsApplicable(Flag flag)
    {
        if (!flag.uniqueFlag) return true; //Jos flag ei ole unique, ei ole väliä vaikka stackaisi

        var flagsearch = AllGlobalFlags.SingleOrDefault(singleflag => singleflag.FlagName == flag.FlagName); //Etsitään kopio, jos edes on, unique check tarpeeton
        return (flagsearch == null) ? true : false; //Jos ei löydy, cond on true, jos löytyy cond on false.
    }
}
