using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicHomeLocation : EventLocation
{
    GameObject dynamicLocationObject;
    string level;
    private void OnEnable()
    {
        try
        {
            level = PlayerDataHolder.playerHome.GetRentableScene();
        }
        catch (System.Exception)
        {

            level = "StudioHomeNo1";
        }

        SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
    }
    // Start is called before the first frame update
    void Start()
    {

    }


}
