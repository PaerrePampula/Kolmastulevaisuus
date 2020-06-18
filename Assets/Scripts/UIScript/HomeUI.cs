using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    private void OnEnable()
    {

        CameraController.OnSceneChange += checkLocation;
    }
    void checkLocation()
    {
        if (LocationHandler.CurrentLocation.getLocation() == FIRE_LOCATION.HOME)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
