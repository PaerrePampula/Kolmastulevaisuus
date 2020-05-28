using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Actions/FloatValueChange")]
public class floatValueChangeAction : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public float amount;
    public override void PerformAction()
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = amount;
        Debug.Log("Pelaaja sai rahaa!, määrä on " + valueChangeAction.changeofFloat);
        GameEventSystem.Current.DoEvent(
            Event_Type,
            valueChangeAction
            );
    }
}
