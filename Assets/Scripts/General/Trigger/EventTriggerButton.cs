using UnityEngine;

public class EventTriggerButton : MonoBehaviour
{
    #region Fields
    [SerializeField]
    ScriptableAction[] eventTriggers;
    [SerializeField]
    Flag[] flags;

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
            if (flags.Length > 0)
            {
                for (int i = 0; i < flags.Length; i++)
                {
                    Flag flag = new Flag(flags[i].FlagName, flags[i].TimeToHappen);
                }
            }

        }
    }
}
