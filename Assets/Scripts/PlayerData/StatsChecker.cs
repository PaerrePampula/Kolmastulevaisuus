using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StatsChecker
{
    #region Fields
    static List<IStattable> currentStats = new List<IStattable>();
    // Start is called before the first frame update
    #endregion
    static StatsChecker()
    {

    }
    public static void RegisterStat(IStattable StatChange)
    {
        
        if (StatChange.UniqueStat)
        {
           var searchForUniqueValue = currentStats.SingleOrDefault(stat => stat.GetType() == StatChange.GetType());

            if (searchForUniqueValue != null) return;
        }
        currentStats.Add(StatChange);
        foreach (var stat in currentStats)
        {
            Debug.Log(stat);
        }
    }
    public static IStattable getPlayerStatByPrereq(PrereqPair pair)
    {
        for (int i = 0; i < currentStats.Count; i++)
        {
            if (currentStats[i].ThisStatType == pair.playerStat)
            {
                return currentStats[i];
            }

        }
        return null;
    }
}
