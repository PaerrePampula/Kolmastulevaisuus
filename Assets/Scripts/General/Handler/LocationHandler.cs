using System.Collections.Generic;
using UnityEngine;

public class LocationHandler : MonoBehaviour
{
    #region Fields
    public List<EventLocation> eventLocations = new List<EventLocation>();
    EventLocation currentLocation;

    public delegate void LocationChange();
    public static event LocationChange OnLocationChange;
    public delegate void TurnEnd();
    public static event TurnEnd OnTurnEnd;

    bool turnEndFlag;

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
    public EventLocation CurrentLocation
    {
        get
        {
            if(currentLocation == null)
            {
                currentLocation = eventLocations[0]; //Aloitetaan ekasta lokaatiosta aina, kun peli alkaa
            }
            return currentLocation;
        }
        set
        {
            currentLocation = value;
        }
    }

    int CurrentIndex => eventLocations.IndexOf(currentLocation);

    void setCurrentLocation(int index)
    {
        Current.currentLocation = eventLocations[index];
    }
    #endregion

    #region MonobehaviourDefaults
    void Start()
    {
        currentLocation = eventLocations[0];


    }
    private void OnEnable()
    {
        GameEventSystem.RegisterListener(Event_Type.CAMERA_TURN, ChangeLocationForward);
        //Kamera kääntyy? Sijainti muuttuu
    }
    private void OnDisable()
    {
        GameEventSystem.UnRegisterListener(Event_Type.CAMERA_TURN, ChangeLocationForward);
        //Kamera kääntyy? Sijainti muuttuu
    }
    #endregion

    public void ChangeLocationForward(EventInfo eventInfo)
    {
        CameraAngleChangeInfo floatChangeInfo = (CameraAngleChangeInfo)eventInfo;
        int newIndex = 0;
        int previousIndex = CurrentIndex;

        if (CurrentIndex + floatChangeInfo.increments <= 3)
        {
            newIndex = CurrentIndex + floatChangeInfo.increments;
        }
        else
        {
            newIndex = (CurrentIndex + floatChangeInfo.increments) % 4; // % operaattori tarkoitaa jakojäännöstä. Korjaa pari väärän sijainnin antamis bugia.
            turnEndFlag = true;
        }

        setCurrentLocation(newIndex);

        OnLocationChange?.Invoke();

        if (hasTurnCompleted())
        {
            OnTurnEnd?.Invoke();
            turnEndFlag = false;
        }
    }
    bool hasTurnCompleted()
    {
        return turnEndFlag ? true : false;
    }

}
