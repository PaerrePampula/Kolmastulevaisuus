using UnityEngine;

public class EventLocation : MonoBehaviour
{
    #region Fields
    public FIRE_LOCATION LOCATION;
    Transform playerSpawnLocation;
    #endregion

    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("SpawnPoint"))
            {
                playerSpawnLocation = transform.GetChild(i);
                return;
            }
        }
    }
    #endregion

    public Transform getSpawnLocation()
    {
        return playerSpawnLocation;
    }
}
