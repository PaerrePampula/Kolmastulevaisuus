using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventTrigger : MonoBehaviour
{
    #region Fields
    Collider collider;
    public ScriptableAction[] TriggerActions;
    #endregion
    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < TriggerActions.Length; i++)
            {
                TriggerActions[i].PerformAction();
            }
        }
    }
    #endregion
}
