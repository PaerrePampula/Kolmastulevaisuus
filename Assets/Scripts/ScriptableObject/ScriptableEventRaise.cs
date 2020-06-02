using UnityEngine;
[CreateAssetMenu(menuName = "Actions/EventRise")]
public class ScriptableEventRaise : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public RandomEventScriptable eventToRise;
    public bool isNotRandomlyChosen;
    public override void PerformAction()
    {
        EventRaise eventRaise = new EventRaise();
        eventRaise.InCaseSpecificEvent = eventToRise;
        eventRaise.SpecificEventRaise = isNotRandomlyChosen;
        GameEventSystem.Current.DoEvent(
            Event_Type,
            eventRaise
            );
    }
}
