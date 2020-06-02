using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Mikä tämä on? Prototyyppi random eventtien tuomisesta ruudulle.
public class EventControl : MonoBehaviour
{
    #region Fields
    GameObject randomEventUIBox; //Boksi joka tuodaan peliin, joka kerta kun eventtiä kutsutaan

    [SerializeField]
    List<RandomEventScriptable> randomEvents; //Kaikki mahdolliset randomeventit scriptableobjekteissa

    List<GameEvent> eventsFromScriptables = new List<GameEvent>(); //ylläoleva listaus käännetty gameevent objekteiksi
    List<GameEvent> filteredListOfEvents = new List<GameEvent>(); //Ylläolevasta listauksesta käännetty filteröity lista kaikille tietyssä sijainnissa mahdollisille eventeille
    #endregion

    #region MonoBehaviourDefaults
    private void Awake()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.TRIGGER_EVENT,
                                                 CreateEventBox); //Event subscribe : Triggeröityy aina trigger_event kutsusta
    }
    void Start()
    {
        randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer");
        //Haetaan random eventtien spawnausta varten valmiiksi luotu mallipohja.

        AggregateScriptablesIntoaNewGameEventList(); 
        //Aggregoidaan eli kootaan kaikki ylläolevassa RandomEvents listassa mainitut objektit uuteen GameEvent listaukseen osina näitä uusia gameeventtejä.

        InvokeAnInitialEventIfPossible(); 
        //Peli aloitetaan random eventillä, jos mahdollista firettää jonkin eventin.

    }
    #endregion

    #region Tool methods
    bool CheckForRaiseChanceIfEligibleEventsCanBeFired() 
        //Tsekkaa jos event raisen aikana pelaajan sijainnissa on yhtäkään eventtiä, jota voi suorittaa
        //event raise = on tietynlainen peli eventti joka nostaa jonkun toisen eventin 
        //(esim triggerin kosketus voisi nostaa eventin, joka kutsuu eventcontrolleria kutsumaan pelaajalle random valittua eventtiä) 
        //ei siis mikään tietty unity-konsepti, vaan osa tätä mun kokkaamaa game-event soppaa, ja yksi monista tavoista ratkaista event-hallinta ongelma unityssä.
    {
        return (filteredListOfEvents.Count > 0) ? true : false;
    }
    void CreateEventBox(EventInfo eventInfo) //Luo eventin peliin ui elementtinä.
    {

        AggregateAppliableEventsForThisLocation(); //aggregoi eli tuo kokoon kaikki eventit jota sijainnisa x voi suorittaa
        if (CheckForRaiseChanceIfEligibleEventsCanBeFired()) //ym. metodi
        {
            EventRaise EventRaise = (EventRaise)eventInfo; //Polymorphismin avulla eventinfosta sopiva käytettävä info

            GameObject randomeventUI = Instantiate(randomEventUIBox); //...jota käytetään tähän tulevaan objektiin
            randomeventUI.transform.SetParent(MainCanvas.mainCanvas.transform) ; //..mutta ensiksi vaihdetaan sen parentiksi meidän UI... (maincanvas on static transform Maincanvaksessa)
            randomeventUI.transform.localPosition = Vector3.zero; //ja nollataan sen sijainti suhteessa "vanhempaan"

            if (EventRaise.SpecificEventRaise == false) //Jos eventti ei ole mikään tietty eventti joka nostettiin, vain täysin random.
            {
                if (filteredListOfEvents.Count > 0) //Jos lista ei ole tyhjä, anna eventille tietoja
                { 
                    randomeventUI.GetComponent<RandomEventUI>().setRandomEvent(filteredListOfEvents[randomizedRandomEventIndexChoice()]);
                    PointAndClickMovement.setMovementStatus(false);

                }
                else
                {

                    //Tämä on aika iso brainfart mun puolesta, tää metodi pitäis laittaa vähän uusiksi mutta leikitään että se on toistaiseksi ihan fine ku tuhotaa tää objekti näin jälkeenpäin :)
                    Destroy(randomeventUI.transform.gameObject);
                    Debug.Log("No events!");
                }
            }
            else
            {
                //Tuodaan tietty eventti, jos raise vaati tiettyä eventtiä.
                GameEvent specificEvent = new GameEvent(EventRaise.InCaseSpecificEvent);
                randomeventUI.GetComponent<RandomEventUI>().setRandomEvent(specificEvent);
            }
        }




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
        for (int i = 0; i < randomEvents.Count; i++)
        {
            GameEvent gameEvent = new GameEvent(randomEvents[i]);
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

    public static void RaiseASpecificEvent(RandomEventScriptable specificRaise)
    {
        EventRaise randomEvent = new EventRaise();
        randomEvent.SpecificEventRaise = true;
        randomEvent.InCaseSpecificEvent = specificRaise;
        GameEventSystem.Current.DoEvent(
            Event_Type.TRIGGER_EVENT,
            randomEvent
            );
    }
    void InvokeAnInitialEventIfPossible() //Nostaa EventRaisen, syöttää sen eteenpäin gaveeventsystemille, joka laittaa siitä viestiä eteenpäin
    {
        EventRaise randomEvent = new EventRaise();
        randomEvent.SpecificEventRaise = false; //tarvitaan täysin randomi paikallinen event, ei ole spesifinen
        GameEventSystem.Current.DoEvent(
            Event_Type.TRIGGER_EVENT,
            randomEvent
            );

    }
}
