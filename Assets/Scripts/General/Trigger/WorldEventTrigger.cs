using UnityEngine;

public class WorldEventTrigger : BaseTrigger
{
    private void OnTriggerEnter(Collider other) //Pelaajan törmäystä varten
    {
        if (other.CompareTag("Player"))
        {
            FireTriggersAndFlags(); //Basetrigger Method
        }
    }
}
