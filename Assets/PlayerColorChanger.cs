using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener(EventSystem.Event_Type.DEBUG_COLOR_CHANGE, changeColor);
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
