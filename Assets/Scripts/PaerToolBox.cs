using UnityEngine;

public static class PaerToolBox //Satunnaisten työkalujen työkaluloota.
{

    public static void callOnStatChange(StatType typeOfStat, bool uniqueness, string statValueString = "", float floatValue = 0)
    {
        StatChangeInfo statChange = new StatChangeInfo();
        statChange.playerStat.statName = typeOfStat;
        statChange.playerStat.statValueString = statValueString;
        statChange.playerStat.
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
    public static void changePlayerMoney(float amount)
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = amount;
        Debug.Log("Pelaajalle tapahtui rahamuutos: määrä on " + valueChangeAction.changeofFloat);
        GameEventSystem.Current.DoEvent(
            Event_Type.FLOAT_CHANGE,
            valueChangeAction
            );
    }
    public static bool isBetween(float comparedValue, float lowerLimit, float upperLimit, bool orEqualsBottom = false)
    {
        return orEqualsBottom ? lowerLimit <= comparedValue && comparedValue < upperLimit //Jos value voi olla myös tasan, mutta ei ylälimitissä (verotus)...
            : lowerLimit < comparedValue && comparedValue < upperLimit; //Jos value ei voi olla myös tasan...
    }
}
