using UnityEngine;

public class WorldEventTrigger : BaseTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FireTriggersAndFlags();
        }
    }
}
