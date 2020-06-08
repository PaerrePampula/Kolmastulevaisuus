using UnityEngine;
[CreateAssetMenu(menuName = "Actions/FloatValueChange")]
public class FloatValueChangeAction : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public float amount;
    public override void PerformAction()
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = amount;
        thisEvent_Type = Event_Type.FLOAT_CHANGE;
        Debug.Log("Pelaajalle tapahtui rahamuutos: määrä on " + valueChangeAction.changeofFloat);
        GameEventSystem.DoEvent(
            thisEvent_Type,
            valueChangeAction
            );
    }
}
