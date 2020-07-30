using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationUIDescriptor : MonoBehaviour
{
    TextMeshProUGUI descText;
    private void OnEnable()
    {
        descText = GetComponent<TextMeshProUGUI>();
        LocationHandler.OnLocationChange += updateLocation;
    }
    private void OnDisable()
    {
        LocationHandler.OnLocationChange -= updateLocation;
    }
    void updateLocation()
    {
        descText.text = LocationHandler.Current.CurrentLocation.ToString();
    }
}
