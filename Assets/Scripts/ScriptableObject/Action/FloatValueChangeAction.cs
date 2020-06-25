using UnityEngine;
[CreateAssetMenu(menuName = "Actions/FloatValueChange")]
public class ExtraIncomeActio : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public float amount;
    public override void PerformAction()
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = amount;
        thisEvent_Type = Event_Type.FLOAT_CHANGE;

        GameEventSystem.DoEvent(
            thisEvent_Type,
            valueChangeAction
            );
    }
}
