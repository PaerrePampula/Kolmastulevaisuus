using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour //Tämä oli vain debugia varten, poistan myöhemmin.
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.DEBUG_COLOR_CHANGE, changeColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void changeColor(EventInfo info)
    {
        ColorChangeInfo colorChangeInfo = (ColorChangeInfo)info;
        gameObject.GetComponent<Renderer>().material.color = colorChangeInfo.newColor;
    }
}
