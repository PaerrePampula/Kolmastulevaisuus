using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//Mikä tämä on? Prototyyppi random eventtien tuomisesta ruudulle.
public class EventControl : MonoBehaviour
{
    GameObject randomEventUIBox;
    public Transform Canvas;
    public List<RandomEventScriptable> RandomEvents;
    List<GameEvent> events = new List<GameEvent>();
    List<GameEvent> filteredList = new List<GameEvent>();
    // Start is called before the first frame update
    private void Awake()
    {

        GameEventSystem.Current.RegisterListener(Event_Type.TRIGGER_EVENT, CreateEventBox);
    }
    void Start()
    {
        randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer"); //Haetaan random eventtien spawnausta varten valmiiksi luotu mallipohja.
        AggregateScriptablesIntoaNewGameEventList(); //Aggregoidaan eli kootaan kaikki ylläolevassa RandomEvents listassa mainitut objektit uuteen GameEvent listaukseen osina näitä uusia gameeventtejä.
        Debug_InvokeAnInitialEvent();

    }
    void Debug_InvokeAnInitialEvent()
    {
        EventRaise randomEvent = new EventRaise();
        randomEvent.SpecificEventRaise = false;
        GameEventSystem.Current.DoEvent(
            Event_Type.TRIGGER_EVENT,
            randomEvent
            );
        
    }
    bool CheckForRaiseChanceIfEligibleEventsCanBeFired()
    {
        if(filteredList.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void CreateEventBox(EventInfo eventInfo) //Tämä on silkkaa debugia, spawnaa random eventin randomisti valittuna. Näkyy heti playmoden avatessa
    {

        AggregateAppliableEventsForThisLocation();
        if (CheckForRaiseChanceIfEligibleEventsCanBeFired())
        {
            EventRaise EventRaise = (EventRaise)eventInfo;

            GameObject randomeventUI = Instantiate(randomEventUIBox);
            randomeventUI.transform.SetParent(Canvas);
            randomeventUI.transform.localPosition = Vector3.zero;

            if (EventRaise.SpecificEventRaise == false)
            {
                if (filteredList.Count > 0)
                {
                    randomeventUI.GetComponent<RandomEventUI>().setRandomEvent(filteredList[randomizedRandomEventIndexChoice()]);
                    PointAndClickMovement.setMovementStatus(false);

                }
                else
                {
                    Debug.Log("No events!");
                }
            }
            else
            {
                randomeventUI.GetComponent<RandomEventUI>().setRandomEvent(filteredList.Find(x => x.getData() == EventRaise.InCaseSpecificEvent));
            }
        }




    }
    int randomizedRandomEventIndexChoice() //Tämä valitsee random eventin halutusta listasta.
    {
            int index = Random.Range(0, filteredList.Count);
            return index;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void AggregateScriptablesIntoaNewGameEventList()
    {
        for (int i = 0; i < RandomEvents.Count; i++)
        {
            GameEvent gameEvent = new GameEvent(RandomEvents[i]);
            events.Add(gameEvent);
        }
    }
    void AggregateAppliableEventsForThisLocation()
    {
        filteredList.Clear();
        filteredList = FindEventsOfLocation();
    }
    //https://www.tutorialsteacher.com/linq/linq-tutorials Esalle tiedoksi, jos LINQ ei ole kovin tuttu
    List<GameEvent> FindEventsOfLocation()
    {
        //LINQ QUERY
        var listofEventsForThisLocationOrAnyLocation = from gameEvent in events //where komennon käyttö löytyy yllämainitusta linkistä standard query operaattoreiden alta
                                                       where (gameEvent.getFireLocation() == LocationHandler.getCurrentLocation().LOCATION) && (gameEvent.CheckPreRequisites() == true) || (gameEvent.getFireLocation() == FIRE_LOCATION.ANY) && (gameEvent.CheckPreRequisites() == true)
                                                       select gameEvent;
        //Eli siis LINQ query, jossa haetaan gameeventtejä (from gameEvent in events = given a gameEvent in the events list...)
        //where gameEvent vastaa tämänhetkistä lokaatiotägiä, tai jos tägi on missä tahansa
        //otetaan valittu event ja lisätään se uuteen listaan.
        
        return listofEventsForThisLocationOrAnyLocation.ToList(); //Palautetaan tämä listana, ei linq queryn outputtina (linq queryn palauttama arvo ei ole sama kuin lista tai joku vastaava collection, todellisen listatyypin näkee sitä pyytämällä koodissa.
    }

}
