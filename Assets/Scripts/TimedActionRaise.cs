using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class TimedActionRaise : IActionable
{
    public Event_Type event_Type;
    public Event_Type thisEvent_Type { get => event_Type; set => event_Type = value; }
    public string Description { get => description; set => description = value; }
    public string description;
    public IInfoable infoable;
    public TimedActionRaise(Event_Type newEventType)
    {
        thisEvent_Type = newEventType;
    }
    public void PerformAction()
    {

        //FloatChangeInfo valueChangeAction = new FloatChangeInfo();

        //thisEvent_Type = Event_Type.FLOAT_CHANGE;

        //GameEventSystem.DoEvent(
        //    thisEvent_Type,
        //    valueChangeAction
        //    );

    }

}