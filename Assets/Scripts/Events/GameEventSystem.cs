using System.Collections.Generic;
using UnityEngine;

public static class GameEventSystem
{
    #region Fields

    public delegate void EventListener(EventInfo e);

    static Dictionary<Event_Type, List<EventListener>> eventListeners; //Tarvitaan dictionary listenereistä, jolle annetaan tietty Event_type enum value, jota kuunnella.
    #endregion

    #region Register and unregisters
    public static void RegisterListener(Event_Type event_Type, EventListener listener)
    {
        if (eventListeners == null) //Jos ei ole listaa olemassa, luo se.
        {
            eventListeners = new Dictionary<Event_Type, List<EventListener>>();
        }

        if (eventListeners.ContainsKey(event_Type) == false || eventListeners[event_Type] == null) //Jos listassa ei ole listenerlistaa tietylle event tyypille, luo se.
        {
            eventListeners[event_Type] = new List<EventListener>();
        }
        eventListeners[event_Type].Add(listener);
    }

    public static void UnRegisterListener(Event_Type event_Type, EventListener listener)
    {
        eventListeners[event_Type].Remove(listener);
    }
    #endregion

    public static void DoEvent(Event_Type event_Type, EventInfo info) //Tätä kutsutaan, kun event firee.
    {
        if (eventListeners == null || eventListeners[event_Type] == null)
        {
            //Kukaan ei kuuntele tämän tyyppisiä eventtejä. Palaa.
            return;
        }
        foreach (EventListener listener in eventListeners[event_Type])
        {
            listener(info); //Eli jokainen eventlistener ottaa eventtiedon, ja suorittaa ne metodit, jotka on listenerille määritelty laukeavan.
        }
    }

}
