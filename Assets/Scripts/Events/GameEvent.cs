﻿using System.Linq;

public class GameEvent
{
    #region Fields
    RandomEventScriptable scriptable; //scriptable, josta haetaan infot.
    bool firedOnce;
    bool isTimed;
    FIRE_LOCATION[] fire_locations; //missä sijainnissa?
    PrereqPair[] prerequisites;
    Flag[] neededFlags;
    int fireTime;
    int fireWeeks; //Viikkoja pelin alusta, kunnes voi fireä
    public delegate void EventSelfTrigger(GameEvent thisEvent);
    public static event EventSelfTrigger OnEventSelfTriggered;
    public delegate void EventAfterTrigger(GameEvent thisEvent);
    public static event EventAfterTrigger AfterTimedEventTriggered;
    #endregion

    #region constructor
    public GameEvent(RandomEventScriptable eventScriptable, bool isThisTimed = false, int timer = 0)
    {
        scriptable = eventScriptable;
        fire_locations = eventScriptable.fire_locations;
        prerequisites = eventScriptable.Prerequisites;

        //Timed event
        isTimed = isThisTimed;
        fireTime = timer;
        firedOnce = eventScriptable.fireOnce;
        fireWeeks = eventScriptable.weeksFromStartForFire;
        if(isTimed)
        {
            LocationHandler.OnTurnEnd += CheckForFiring;
        }
        //Timed event
        ResetButton.onReset += unInit;
    }
    #endregion
    void unInit()
    {
        LocationHandler.OnTurnEnd -= CheckForFiring;
    }
    

    public RandomEventScriptable getData() //Hakee siis scriptablen eventistä tiedonhallintaa varten.
    {
        return scriptable;
    }
    public FIRE_LOCATION[] getFireLocations()
    {
        return fire_locations;
    }
    public void CheckForFiring()
    {
        fireTime--;

        if ((fireTime <= 0) && (fire_locations.Contains(LocationHandler.Current.CurrentLocation.getLocation()) || fire_locations.Contains(FIRE_LOCATION.ANY)))
        {
            OnEventSelfTriggered.Invoke(this);
            LocationHandler.OnTurnEnd -= CheckForFiring;
        }
    }
    public bool checkPreRequisites()
    {
        bool check = false;
        if (DateTimeSystem.GameStartDate.AddDays(7*fireWeeks) > DateTimeSystem.getCurrentDate())
        {
            return false;
        }
        if (prerequisites.Length < 1)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < prerequisites.Length; i++)
            {
                check = (prerequisites[i].CheckPreRequisites() == true) ? true : false;
            }
        }
        return check;

    }
}
