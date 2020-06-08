using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Actions/TimedEventRise")]
public class TimedEventRaise : ScriptableAction
{
    
    public RandomEventScriptable eventToRise;
    public int eventFireTime;
    public override void PerformAction()
    {
        EventRegisterInfo eventRegister = new EventRegisterInfo();
        GameEvent @event = new GameEvent(eventToRise, true, eventFireTime);
        eventRegister.Event = @event;
        thisEvent_Type = Event_Type.EVENT_REGISTER;
        GameEventSystem.DoEvent(
            thisEvent_Type,
            eventRegister
            );
    }
}
