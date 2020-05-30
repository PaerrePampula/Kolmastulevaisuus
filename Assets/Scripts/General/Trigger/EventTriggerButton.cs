using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerButton : MonoBehaviour
{
    #region Fields
    public ScriptableAction[] eventTriggers;
    #endregion
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
}
