using UnityEngine;

public static class PaerToolBox //Satunnaisten työkalujen työkaluloota.
{

    public static void callNonUniqueStatChange(IStattable stattable)
    {
        StatsChecker.RegisterStat(stattable);
    }

    public static void changePlayerMoney(float amount)
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = amount;
        Debug.Log("Pelaajalle tapahtui rahamuutos: määrä on " + valueChangeAction.changeofFloat);
        GameEventSystem.DoEvent(
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
