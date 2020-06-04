using UnityEngine;
using UnityEngine.Events;

public class WorldInteractive : MonoBehaviour, IHoverable
{
    #region Fields
    public UnityEvent[] clickEvents;
    [SerializeField]
    string propName;
    public delegate void InteractHover(bool hoverstate, string text = "", Transform transform = null);
    public static event InteractHover OnHover;
    public string getHoverName()
    {
        return propName;
    }
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
            OnHover.Invoke(false);
        }

    }
    private void OnMouseEnter()
    {
        if (!MainCanvas.mainCanvas.isUIOverride)
        {
            OnHover.Invoke(true, propName, this.transform);
        }

    }
    private void OnMouseExit()
    {
        if (!MainCanvas.mainCanvas.isUIOverride)
        {
            OnHover.Invoke(false);
        }

    }
    #endregion
}
