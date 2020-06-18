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
    void checkLocation()
    {

        if (LocationHandler.CurrentLocation.getLocation() == FIRE_LOCATION.HOME)
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
