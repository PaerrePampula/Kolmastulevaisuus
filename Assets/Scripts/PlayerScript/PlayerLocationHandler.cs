using UnityEngine;
using UnityEngine.AI;

public class PlayerLocationHandler : MonoBehaviour
{
    #region Fields
    [SerializeField]
    NavMeshAgent playerAgent;
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
        playerAgent.Warp(LocationHandler.Current.CurrentLocation.getSpawnLocation().position);
    }
    void storePlayer(bool isBegin, bool isObjectChange = false)
    {
        switch (isBegin)
        {
            case true:
                if (isObjectChange == false)
                {
                    currentLocation = playerAgent.transform.position;
                    playerAgent.Warp(Vector3.zero);

                }

                break;
            default:
                playerAgent.Warp(currentLocation);

                break;
        }
    }
}
