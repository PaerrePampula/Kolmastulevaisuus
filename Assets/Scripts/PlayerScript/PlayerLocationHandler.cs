using UnityEngine;
using UnityEngine.AI;

public class PlayerLocationHandler : MonoBehaviour
{
    #region Fields
    public NavMeshAgent playerAgent;
    Transform locationTransform;
    #endregion

    #region MonobehaviourDefaults
    private void OnEnable()
    {
        LocationHandler.OnLocationChange += SpawnPlayerCharacterToNewLocation;
    }
    private void OnDisable()
    {
        LocationHandler.OnLocationChange -= SpawnPlayerCharacterToNewLocation;
    }
    #endregion
    void SpawnPlayerCharacterToNewLocation()
    {
        playerAgent.Warp(LocationHandler.CurrentLocation.getSpawnLocation().position);
    }
}
