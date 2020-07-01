using UnityEngine;

public class EventLocation : MonoBehaviour //Eventlocation behaviour löytyy jokaisesta sijainnin containerista, jota meidän scenessä on yht 4. joka ilmansuunnassa.
    //ne määrittelee pääasiassa minkätyyppisiä sijainteja joka lohkosta löytyy, tätä toiminnallisuutta olisi varmaan ihan kiva muuttaa niinkin että pelaajalla on mahdollisuus
    //vaihdella vähän lohkoja niin, että työlohko voisi olla välillä vaikka joku "käy kaupungilla heilumassa" lohko.
{
    #region Fields
    [SerializeField]
    FIRE_LOCATION _location; //Määritelty locationtyyppi
    string locationFadeOutText;
    [SerializeField]
    Transform playerSpawnLocation;

    public string GetLocationFadeOutText()
    {
        return locationFadeOutText;
    }

    public void SetLocationFadeOutText(string value)
    {
        locationFadeOutText = value;
    }
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
    public FIRE_LOCATION getLocation()
    {


        return _location;
    }
    public void setLocation(FIRE_LOCATION fire_location)
    {
        _location = fire_location;
    }
    public Transform getSpawnLocation()
    {
        return playerSpawnLocation;
    }
}
