using UnityEngine;
using System.Collections;

public static class PaerToolBox
{

    public static void callOnStatChange(StatType typeOfStat, string statValue, bool uniqueness)
    {
        StatChangeInfo statChange = new StatChangeInfo();
        statChange.playerStat.statName = typeOfStat;
        statChange.playerStat.statValueString = statValue;
        statChange.playerStat.uniqueStat = uniqueness;
        GameEventSystem.Current.DoEvent(
            Event_Type.STATS_CALL,
            statChange
        );
    }
    public static void callOnStatChange(StatType typeOfStat, bool booleanOperator, bool uniqueness)
    {
        StatChangeInfo statChange = new StatChangeInfo();
        statChange.playerStat.statName = typeOfStat;
        statChange.playerStat.booleanValue = booleanOperator;
        statChange.playerStat.uniqueStat = uniqueness;
        GameEventSystem.Current.DoEvent(
            Event_Type.STATS_CALL,
            statChange
        );
    }
}
