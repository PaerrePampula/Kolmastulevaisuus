using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    #region Fields
    static private GameEventSystem _Current; //Tietenkin tämänhetkinen eventsystem. On static, koska silloin sitä voi käsitellä mistä tahansa koodissa.
    static public GameEventSystem Current
    {
        get
        {
            if(_Current == null)
            {
                _Current = FindObjectOfType<GameEventSystem>();
            }
            return _Current;
        }
    }
    public delegate void EventListener(EventInfo e);

    Dictionary<Event_Type, List<EventListener>> eventListeners; //Tarvitaan dictionary listenereistä, jolle annetaan tietty Event_type enum value, jota kuunnella.
    #endregion

    #region Register and unregisters
    public void RegisterListener(Event_Type event_Type, EventListener listener)
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

    public void UnRegisterListener(Event_Type event_Type, EventListener listener)
    {
        //TODO: Tämä toiminto. Tarkoitus on poistaa listener käytöstä sen takia että ei ole kuuntelijoita objekteille tai toiminnoille, jotka ovat poistettu pelin aikana.
    }
    #endregion

    public void DoEvent(Event_Type event_Type, EventInfo info) //Tätä kutsutaan, kun event firee.
    {
        if (eventListeners == null || eventListeners[event_Type] == null)
        {
            //Kukaan ei kuuntele. Palaa.
            return;
        }
        foreach (EventListener listener in eventListeners[event_Type])
        {
            listener(info); //Eli jokainen eventlistener ottaa eventtiedon, ja suorittaa ne metodit, jotka on listenerille määritelty laukeavan.
        }
    }

}
