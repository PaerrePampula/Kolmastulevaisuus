using UnityEngine;
using UnityEngine.Events;

public class WorldInteractive : MonoBehaviour
{
    #region Fields
    public UnityEvent[] clickEvents;
    #endregion
    #region MonobehaviourDefaults
    private void OnMouseDown()
    {
        if (!MainCanvas.mainCanvas.isUIOverride)
        {
            for (int i = 0; i < clickEvents.Length; i++)
            {
                clickEvents[i].Invoke();
            }
        }

    }
    #endregion
}
