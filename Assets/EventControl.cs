using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControl : MonoBehaviour
{
    GameObject randomEventUIBox;
    public Transform canvas;
    public List<RandomEventScriptable> schoolRandomEvents;
    // Start is called before the first frame update
    void Start()
    {
        randomEventUIBox = Resources.Load<GameObject>("RandomEventContainer");
        DEBUGCreateEventBox();

    }
    void DEBUGCreateEventBox() //Tämä on silkkaa debugia, spawnaa random eventin randomisti valittuna.
    {
        GameObject randomevent = Instantiate(randomEventUIBox);
        randomevent.transform.SetParent(canvas);
        randomevent.GetComponent<RandomEventUI>().setRandomEvent(schoolRandomEvents[randomizedRandomEventIndexChoice(schoolRandomEvents)]);
        randomevent.transform.localPosition = Vector3.zero;
    }
    int randomizedRandomEventIndexChoice(List<RandomEventScriptable> chosenlist)
    {
        int index = Random.Range(0, chosenlist.Count + 1);
        return index;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
