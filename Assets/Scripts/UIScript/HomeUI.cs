using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    [SerializeField]
    Transform[] buttons;
    private void OnEnable()
    {
        setChildrenStatus(false);
        LocationHandler.OnLocationChange += checkLocation;

    }
    private void OnDisable()
    {
        LocationHandler.OnLocationChange -= checkLocation;
    }
    void checkLocation()
    {

        if (LocationHandler.Current.CurrentLocation.getLocation() == FIRE_LOCATION.HOME)
        {
            setChildrenStatus(true);

        }
        else
        {
            setChildrenStatus(false);
        }
    }
    void setChildrenStatus(bool status)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(status);
        };
    }
}
