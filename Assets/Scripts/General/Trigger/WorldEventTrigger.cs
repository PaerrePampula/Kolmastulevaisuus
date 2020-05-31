using UnityEngine;

public class WorldEventTrigger : MonoBehaviour
{
    #region Fields
    public ScriptableAction[] TriggerActions;
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < TriggerActions.Length; i++)
            {
                TriggerActions[i].PerformAction();
            }
        }
    }
}
