using UnityEngine;

public static class PaerToolBox //Satunnaisten työkalujen työkaluloota.
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
    public static void giveMoneyToPlayer(float amount)
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = amount;
        Debug.Log("Pelaaja sai rahaa!, määrä on " + valueChangeAction.changeofFloat);
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
