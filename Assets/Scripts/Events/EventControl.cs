using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Mikä tämä on? Prototyyppi random eventtien tuomisesta ruudulle.
public class EventControl : MonoBehaviour
{
    #region Fields
    GameObject randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer"); 
    //Boksi joka tuodaan peliin, joka kerta kun eventtiä kutsutaan

    [SerializeField]
    List<RandomEventScriptable> RandomEvents; //Kaikki mahdolliset randomeventit scriptableobjekteissa

    List<GameEvent> eventsFromScriptables = new List<GameEvent>(); //ylläoleva listaus käännetty gameevent objekteiksi
    List<GameEvent> filteredListOfEvents = new List<GameEvent>(); //Ylläolevasta listauksesta käännetty filteröity lista kaikille tietyssä sijainnissa mahdollisille eventeille


    #endregion

    #region MonoBehaviourDefaults
    private void Awake()
    {
        GameEventSystem.RegisterListener(Event_Type.TRIGGER_EVENT,
                                                 CreateEventBox); //Event subscribe : Triggeröityy aina trigger_event kutsusta
    }
    void Start()
    {
        AggregateScriptablesIntoaNewGameEventList();
        //Aggregoidaan eli kootaan kaikki ylläolevassa RandomEvents listassa mainitut objektit uuteen GameEvent listaukseen osina näitä uusia gameeventtejä.

        RaiseAnEvent();
        //Peli aloitetaan random eventillä, jos mahdollista firettää jonkin eventin.

    }
    #endregion

    #region Tool methods
    bool CheckForRaiseChanceIfEligibleEventsCanBeFired() 
        //Tarkistaa, että onko nyk. sijainnissa mahdollisia eventtejä
    {
        return (filteredListOfEvents.Count > 0) ? true : false;
    }
    void CreateEventBox(EventInfo eventInfo) //Luo eventin peliin ui elementtinä.
    {

        AggregateAppliableEventsForThisLocation(); //aggregoi eli tuo kokoon kaikki eventit jota sijainnisa x voi suorittaa
        if (CheckForRaiseChanceIfEligibleEventsCanBeFired()) //ym. metodi
        {
            EventRaise EventRaise = (EventRaise)eventInfo; //Polymorphismin avulla eventinfosta sopiva käytettävä info

            var go = Instantiate(randomEventUIBox);
            var randomeventUI = go; //...jota käytetään tähän tulevaan objektiin

            randomeventUI.transform.SetParent(MainCanvas.mainCanvas.transform); //..mutta ensiksi vaihdetaan sen parentiksi meidän UI... (maincanvas on static transform Maincanvaksessa)
            randomeventUI.transform.localPosition = Vector3.zero; //ja nollataan sen sijainti suhteessa "vanhempaan"

            if (EventRaise.SpecificEventRaise == false) //Jos eventti ei ole mikään tietty eventti joka nostettiin, vain täysin random.
            {
                if (filteredListOfEvents.Count > 0) //Jos lista ei ole tyhjä, anna eventille tietoja
                { 
                    randomeventUI.GetComponent<RandomEventUI>()
                        .Init(filteredListOfEvents[randomizedRandomEventIndexChoice()]);

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
                var specificEvent = new GameEvent(EventRaise.InCaseSpecificEvent);
                randomeventUI.GetComponent<RandomEventUI>()
                             .Init(specificEvent);
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

}
