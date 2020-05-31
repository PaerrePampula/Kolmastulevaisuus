using UnityEngine;

public class EventTriggerButton : MonoBehaviour
{
    #region Fields
    [SerializeField]
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
