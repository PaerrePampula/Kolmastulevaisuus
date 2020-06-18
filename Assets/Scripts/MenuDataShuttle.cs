using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuDataShuttle : MonoBehaviour
{
    public RentableHome shuttledHome;
    // Use this for initialization
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Start()
    {
        PlayerDataHolder.playerHome = shuttledHome;
        Debug.Log(PlayerDataHolder.playerHome.Address);
        Destroy(gameObject);

    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}
