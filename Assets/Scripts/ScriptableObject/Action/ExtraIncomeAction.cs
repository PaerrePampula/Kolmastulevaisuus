using UnityEngine;
[CreateAssetMenu(menuName = "Actions/ExtraIncome")]
public class ExtraIncomeAction : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public float amount;
    public override void PerformAction()
    {
        ExtraIncomeInfo valueChangeAction = new ExtraIncomeInfo();
        valueChangeAction.extraIncomeAmount = amount;
        valueChangeAction.isAnExtra = true;
        thisEvent_Type = Event_Type.EXTRA_INCOME;

        GameEventSystem.DoEvent(
            thisEvent_Type,
            valueChangeAction
            );
    }
}
