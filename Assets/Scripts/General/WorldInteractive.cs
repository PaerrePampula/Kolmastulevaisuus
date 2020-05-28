using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldInteractive : MonoBehaviour
{
    public UnityEvent[] clickEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (!MainCanvas.mainCanvas.isUIOverride)
        {
            for (int i = 0; i < clickEvents.Length; i++)
            {
                clickEvents[i].Invoke();
            }
        }

    }
}
