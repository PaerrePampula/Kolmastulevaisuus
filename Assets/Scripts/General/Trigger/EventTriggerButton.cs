using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerButton : MonoBehaviour
{
    public ScriptableAction[] eventTriggers;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TriggerEvents()
    {
        if (eventTriggers.Length <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < eventTriggers.Length; i++)
            {
                eventTriggers[i].PerformAction();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
