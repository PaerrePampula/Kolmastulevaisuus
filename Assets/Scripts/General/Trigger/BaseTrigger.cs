using UnityEngine;
using System.Collections;

public class BaseTrigger : MonoBehaviour
{
    [SerializeField]
    public ScriptableAction[] eventTriggers;

    [SerializeField]
    public Flag[] flags;

    [SerializeField]
    public CustomAction[] customActions;

    public virtual void FireTriggersAndFlags() //Kutsutaan kaikki ScriptableActionit sekä Global flag callit
    {
        try
        {
            for (int i = 0; i < eventTriggers.Length; i++)
            {

                eventTriggers[i].PerformAction();

            }
            for (int i = 0; i < flags.Length; i++)
            {

                flags[i].FireFlag();

            }
            for (int i = 0; i < customActions.Length; i++)
            {
                customActions[i].PerformAction();
            }
        }
        catch (System.NullReferenceException)
        {
            return;
        }



    }
}
