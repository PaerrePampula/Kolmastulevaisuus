using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Mikä tämä on? Prototyyppi random eventtien tuomisesta ruudulle.
public class EventControl : MonoBehaviour
{
    GameObject randomEventUIBox;
    public Transform canvas;
    public List<RandomEventScriptable> schoolRandomEvents;
    // Start is called before the first frame update
    void Start()
    {
        randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer"); //Haetaan random eventtien spawnausta varten valmiiksi luotu mallipohja.
        DEBUGCreateEventBox(); //DEBUGIA.

    }

    void DEBUGCreateEventBox() //Tämä on silkkaa debugia, spawnaa random eventin randomisti valittuna. Näkyy heti playmoden avatessa
    {
        GameObject randomevent = Instantiate(randomEventUIBox);
        randomevent.transform.SetParent(canvas);
        randomevent.GetComponent<RandomEventUI>().setRandomEvent(schoolRandomEvents[randomizedRandomEventIndexChoice(schoolRandomEvents)]);
        randomevent.transform.localPosition = Vector3.zero;
    }
    int randomizedRandomEventIndexChoice(List<RandomEventScriptable> chosenlist) //Tämä valitsee random eventin halutusta listasta.
    {
        int index = Random.Range(0, chosenlist.Count -1);
        return index;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
