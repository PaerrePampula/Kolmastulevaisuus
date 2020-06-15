using UnityEngine;
using UnityEngine.AI;

public class PlayerLocationHandler : MonoBehaviour
{
    #region Fields
    public NavMeshAgent playerAgent;
    Transform locationTransform;
    Vector3 currentLocation;

    #endregion

    #region MonobehaviourDefaults
    private void OnEnable()
    {
        LocationHandler.OnLocationChange += SpawnPlayerCharacterToNewLocation;
        PlacementHelper.OnPlacementInteract += storePlayer;
    }
    private void OnDisable()
    {
        LocationHandler.OnLocationChange -= SpawnPlayerCharacterToNewLocation;
        PlacementHelper.OnPlacementInteract -= storePlayer;
    }
    #endregion
    void SpawnPlayerCharacterToNewLocation()
    {
        playerAgent.Warp(LocationHandler.CurrentLocation.getSpawnLocation().position);
    }
    void storePlayer(bool isBegin)
    {
        switch (isBegin)
        {
            case true:
                currentLocation = playerAgent.transform.position;
                playerAgent.Warp(Vector3.zero);
                break;
            default:
                playerAgent.Warp(currentLocation);
                break;
        }
    }
}
