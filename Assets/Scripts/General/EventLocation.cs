using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLocation : MonoBehaviour
{
    public FIRE_LOCATION LOCATION;
    Transform playerSpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).CompareTag("SpawnPoint"))
            {
                playerSpawnLocation = transform.GetChild(i);
                return;
            }
        }
    }
    public Transform getSpawnLocation()
    {
        return playerSpawnLocation;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
