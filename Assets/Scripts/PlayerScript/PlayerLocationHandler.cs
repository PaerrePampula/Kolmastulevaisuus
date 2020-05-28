using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerLocationHandler : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    Transform locationTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        LocationHandler.OnLocationChange += SpawnPlayerCharacterToNewLocation;
    }
    private void OnDisable()
    {
        LocationHandler.OnLocationChange -= SpawnPlayerCharacterToNewLocation;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnPlayerCharacterToNewLocation()
    {
        playerAgent.Warp(LocationHandler.getCurrentLocation().getSpawnLocation().position);
    }
}
