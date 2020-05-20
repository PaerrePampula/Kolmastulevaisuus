using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    static private EventSystem _Current;
    static public EventSystem Current
    {
        get
        {
            if(_Current == null)
            {
                _Current = GameObject.FindObjectOfType<EventSystem>();
            }
            return _Current;
        }
    }
    public delegate void EventListener(EventInfo e);
    public enum Event_Type
    {
        FLOAT_CHANGE,
        FLOAT_SET,
        JOB_APPLY,
        JOB_FIRED,
        DEBUG_COLOR_CHANGE

    }
    Dictionary<Event_Type, List<EventListener>> eventListeners;


    public void RegisterListener(Event_Type event_Type, EventListener listener)
    {
        if (eventListeners == null)
        {
            eventListeners = new Dictionary<Event_Type, List<EventListener>>();


        }

        if (eventListeners.ContainsKey(event_Type) == false || eventListeners[event_Type] == null)
        {
            eventListeners[event_Type] = new List<EventListener>();
        }
        eventListeners[event_Type].Add(listener);
    }

    public void UnRegisterListener(Event_Type event_Type, EventListener listener)
    {

    }

    public void DoEvent(Event_Type event_Type, EventInfo info)
    {
        if (eventListeners == null || eventListeners[event_Type] == null)
        {
            //Kukaan ei kuuntele. Palaa.
            return;
        }
        foreach (EventListener listener in eventListeners[event_Type])
        {
            listener(info);
        }
    }

}
