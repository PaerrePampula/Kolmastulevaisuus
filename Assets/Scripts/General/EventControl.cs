using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Mikä tämä on? Prototyyppi random eventtien tuomisesta ruudulle.
public class EventControl : MonoBehaviour
{
    GameObject randomEventUIBox;
    public Transform Canvas;
    public List<RandomEventScriptable> RandomEvents;
    List<GameEvent> events = new List<GameEvent>();
    // Start is called before the first frame update
    void Start()
    {
        randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer"); //Haetaan random eventtien spawnausta varten valmiiksi luotu mallipohja.
        AggregateScriptablesIntoaNewGameEventList(); //Aggregoidaan eli kootaan kaikki ylläolevassa RandomEvents listassa mainitut objektit uuteen GameEvent listaukseen osina näitä uusia gameeventtejä.
        DEBUGCreateEventBox(); //DEBUGIA.

    }
    
    void DEBUGCreateEventBox() //Tämä on silkkaa debugia, spawnaa random eventin randomisti valittuna. Näkyy heti playmoden avatessa
    {
        GameObject randomeventUI = Instantiate(randomEventUIBox);
        randomeventUI.transform.SetParent(Canvas);
        randomeventUI.GetComponent<RandomEventUI>().setRandomEvent(events[randomizedRandomEventIndexChoice(events)]);
        randomeventUI.transform.localPosition = Vector3.zero;
    }
    int randomizedRandomEventIndexChoice(List<GameEvent> chosenlist) //Tämä valitsee random eventin halutusta listasta.
    {
        int index = Random.Range(0, chosenlist.Count -1);
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

}
