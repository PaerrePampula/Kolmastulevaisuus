using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Actions/EventRise")]
public class ScriptableEventRaise : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public ScriptableAction actionToRise;
    public bool isNotRandomlyChosen;
    public override void PerformAction()
    {
        EventRaise eventRaise = new EventRaise();
        eventRaise.InCaseSpecificEvent = actionToRise;
        eventRaise.SpecificEventRaise = isNotRandomlyChosen;
        EventSystem.Current.DoEvent(
            Event_Type,
            eventRaise
            );
    }
}
