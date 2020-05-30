﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationHandler : MonoBehaviour
{
    #region Fields
    public List<EventLocation> eventLocations;
    static EventLocation currentLocation;

    public delegate void LocationChange();
    public static event LocationChange OnLocationChange;
    static private LocationHandler _Current;
    static public LocationHandler Current
    {
        get
        {
            if (_Current == null)
            {
                _Current = FindObjectOfType<LocationHandler>();
            }
            return _Current;
        }
    }
    #endregion

    #region Getters and setters
    public static EventLocation getCurrentLocation() //Static, sillä ei pelaaja voi olla kahdessa paikkaa, sekä olisi kiva, että tämän tiedon saisi haettua melkein mistä vaan koodissa
    {
        return currentLocation;
    }
    int getCurrentIndex() //Käyttöä sijainninvaihdossa.
    {
        return eventLocations.IndexOf(currentLocation);
    }
    void setCurrentLocation(int index)
    {
        currentLocation = eventLocations[index];
        Debug.Log(currentLocation.LOCATION);

    }
    #endregion

    #region MonobehaviourDefaults
    // Start is called before the first frame update
    //Tämä on handleri sijainnintallennukseen, olennaista, kun pelaaja saa eventtejä.
    void Awake()
    {
        currentLocation = eventLocations[0];

        GameEventSystem.Current.RegisterListener(Event_Type.CAMERA_TURN, ChangeLocationForward);


    }
    #endregion

    public void ChangeLocationForward(EventInfo eventInfo)
    {
        CameraAngleChangeInfo floatChangeInfo = (CameraAngleChangeInfo)eventInfo;
        int newIndex = 0;
        if (getCurrentIndex() + floatChangeInfo.increments < 3)
        {
            newIndex = getCurrentIndex() + floatChangeInfo.increments;
        }
        else
        {
            newIndex = (getCurrentIndex() + floatChangeInfo.increments) % 4; //& = Modulo. Jakojäännös. 4 % 4 = 0, 4 % 5 = 1, 2 % 4 = 2 jne...
        }
        setCurrentLocation(newIndex);
        OnLocationChange?.Invoke();

    }

}
