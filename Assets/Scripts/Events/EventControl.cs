using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Mikä tämä on? Prototyyppi random eventtien tuomisesta ruudulle.
public class EventControl : MonoBehaviour
{
    #region Fields
    GameObject randomEventUIBox;
    //Boksi joka tuodaan peliin, joka kerta kun eventtiä kutsutaan

    [SerializeField]
    List<RandomEventScriptable> RandomEvents; //Kaikki mahdolliset randomeventit scriptableobjekteissa

    List<GameEvent> eventsFromScriptables = new List<GameEvent>(); //ylläoleva listaus käännetty gameevent objekteiksi
    List<GameEvent> filteredListOfEvents = new List<GameEvent>(); //Ylläolevasta listauksesta käännetty filteröity lista kaikille tietyssä sijainnissa mahdollisille eventeille
    List<GameEvent> toTriggerEvents = new List<GameEvent>();
    Queue<GameEvent> eventQueue = new Queue<GameEvent>();


    GameObject eventOnDisplay = null;
    public bool testBool = false;
    #endregion

    #region MonoBehaviourDefaults
    private void Awake()
    {
        GameEventSystem.RegisterListener
            (Event_Type.TRIGGER_EVENT,
             CallEventWithEventInfo); //Event subscribe : Triggeröityy aina trigger_event kutsusta
        GameEventSystem.RegisterListener(
            Event_Type.EVENT_REGISTER,
            RegisterEventToBeQueued);

        randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer");
    }
    void Start()
    {
        AggregateScriptablesIntoaNewGameEventList();
        //Aggregoidaan eli kootaan kaikki ylläolevassa RandomEvents listassa mainitut objektit uuteen GameEvent listaukseen osina näitä uusia gameeventtejä.
        AggregateAppliableEventsForThisLocation();

        RaiseAnEvent();
        //Peli aloitetaan random eventillä, jos mahdollista firettää jonkin eventin.

    }
    private void OnEnable()
    {
        CameraController.OnSceneChange += AggregateAppliableEventsForThisLocation;
        GameEvent.OnEventSelfTriggered += startTriggeringTimedEvent;
        GameEvent.AfterTimedEventTriggered += UnRegisterEventFromQueue; 
    }
    private void OnDisable()
    {
        CameraController.OnSceneChange -= AggregateAppliableEventsForThisLocation;
        GameEvent.OnEventSelfTriggered -= startTriggeringTimedEvent;
        GameEvent.AfterTimedEventTriggered -= UnRegisterEventFromQueue;
    }
    #endregion

    #region Tool methods
    bool CheckForRaiseChanceIfEligibleEventsCanBeFired() 
        //Tarkistaa, että onko nyk. sijainnissa mahdollisia eventtejä
    {
        AggregateAppliableEventsForThisLocation();
        return (filteredListOfEvents.Count > 0) ? true : false;
    }
    void startTriggeringTimedEvent(GameEvent newEvent)
    {
        TriggerEvent(newEvent);
    }

    void TriggerEvent(GameEvent newEvent) //Luo eventin peliin ui elementtinä.
    {

            GameObject go = Instantiate(randomEventUIBox);
            RandomEventUI randomeventUI = go.GetComponent<RandomEventUI>();
            eventOnDisplay = go;


            go.transform.SetParent(MainCanvas.mainCanvas.transform); //..mutta ensiksi vaihdetaan sen parentiksi meidän UI... (maincanvas on static transform Maincanvaksessa)
            go.transform.localPosition = Vector3.zero; //ja nollataan sen sijainti suhteessa "vanhempaan"
            randomeventUI.Init(newEvent);

            PointAndClickMovement.setMovementStatus(false);



    }
    GameEvent getRandomizedEvent()
    {

        return filteredListOfEvents[randomizedRandomEventIndexChoice()] != null ? filteredListOfEvents[randomizedRandomEventIndexChoice()] : null;

    }
    int randomizedRandomEventIndexChoice() //Tämä valitsee random eventin halutusta listasta.
    {
        int index = Random.Range(0, filteredListOfEvents.Count);
        return index;

    }
    #endregion

    #region Event Aggregation and collection
    void AggregateScriptablesIntoaNewGameEventList() //scriptablet poimitaan listasta GameEvent constructoria varten rakentamaan game-eventtien sisällöt, jonka jälkeen ne listataan täällä events listassa
        //Ei haluta käyttä scriptableobjkteja itse jonakin yhtä fyysisenä asiana koodissa kuin mikä tahansa muu class, mutta data-säiliönä jonka tiedot poimitaan käyttöön.
    {
        for (int i = 0; i < RandomEvents.Count; i++)
        {
            var gameEvent = new GameEvent(RandomEvents[i]);
            eventsFromScriptables.Add(gameEvent);
        }
    }
    void AggregateAppliableEventsForThisLocation() //Poimitaan events listasta kaikki ne eventit, jota nyk. sijainnissa voi firettää sekä tsekataan, että onko kaikki ehdot täyttynyt eventin triggeröitymiseen.
    {
        filteredListOfEvents.Clear();
        filteredListOfEvents = FindEventsOfLocation();
    }
    //https://www.tutorialsteacher.com/linq/linq-tutorials Esalle tiedoksi, jos LINQ ei ole kovin tuttu
    List<GameEvent> FindEventsOfLocation()
    {
        //LINQ QUERY
        var listofEventsForThisLocationOrAnyLocation = from gameEvent in eventsFromScriptables //where komennon käyttö löytyy yllämainitusta linkistä standard query operaattoreiden alta
                                                       where (gameEvent.getFireLocations().Contains(LocationHandler.CurrentLocation.getLocation()) == true
                                                       && (gameEvent.CheckPreRequisites() == true) || (gameEvent.getFireLocations().Contains(FIRE_LOCATION.ANY)) == true  
                                                       && (gameEvent.CheckPreRequisites() == true))
                                                       select gameEvent;
        //Eli siis LINQ query, jossa haetaan gameeventtejä (from gameEvent in events = given a gameEvent in the events list...)
        //where gameEvent vastaa tämänhetkistä lokaatiotägiä, tai jos tägi on missä tahansa
        //otetaan valittu event ja lisätään se uuteen listaan.
        //LINQ:iä voi hyödyntää vähän tyypillisemmällä tutumman näköisellä syntaksilla esim:
        //var Poimittuja = events.Where(gameevent => gameevent.Muuttuja == jokuMuuttuja).ToList();
        
        return listofEventsForThisLocationOrAnyLocation.ToList(); //Palautetaan tämä listana, ei linq queryn outputtina (linq queryn palauttama arvo ei ole sama kuin lista tai joku vastaava collection, todellisen listatyypin näkee sitä pyytämällä koodissa.
    }
    #endregion

    public static void RaiseAnEvent(RandomEventScriptable raise = null, bool specificCheck = false)
    {
        EventRaise randomEvent = new EventRaise
        {
            SpecificEventRaise = specificCheck,
            InCaseSpecificEvent = raise
        };

            GameEventSystem.DoEvent(
            Event_Type.TRIGGER_EVENT,
            randomEvent
            );
    }
    void RegisterEventToBeQueued(EventInfo @event)
    {
        EventRegisterInfo toRegisterEventInfo = (EventRegisterInfo)@event;
        GameEvent toRegisterEvent = toRegisterEventInfo.Event;
        toTriggerEvents.Add(toRegisterEvent);
    }
    void UnRegisterEventFromQueue(GameEvent @event)
    {
        toTriggerEvents.Remove(@event);
    }
    void CallEventWithEventInfo(EventInfo eventInfo)
    {
        EventRaise raise = (EventRaise)eventInfo;
        GameEvent gameEvent = null;
        if(raise.SpecificEventRaise)
        {
            gameEvent = new GameEvent(raise.InCaseSpecificEvent);
        }
        if (!raise.SpecificEventRaise && CheckForRaiseChanceIfEligibleEventsCanBeFired())
        {

            gameEvent = getRandomizedEvent();


        }
        if (gameEvent != null)
        {
            TriggerEvent(gameEvent);
        }

    }
}
