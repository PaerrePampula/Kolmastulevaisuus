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
        }

    }
    private void OnMouseEnter()
    {
        OnHover.Invoke(true, propName, this.transform);
    }
    private void OnMouseExit()
    {
        OnHover.Invoke(false);
    }
    #endregion
}
