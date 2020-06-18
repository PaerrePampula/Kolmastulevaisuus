using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTriggerer : MonoBehaviour
{
    [SerializeField]
    Flag[] flagsToTrigger;
    public void triggerFlags()
    {
        foreach (var item in flagsToTrigger)
        {
            if ((GlobalGameFlags.GetFlag(item.FlagName) != null) && item.uniqueFlag == true)
            {
                return;
            }
            item.FireFlag();
        }
    }
}
