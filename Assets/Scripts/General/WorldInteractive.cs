﻿using UnityEngine;
using UnityEngine.Events;

public class WorldInteractive : MonoBehaviour, IHoverable
{
    #region Fields
    public UnityEvent[] clickEvents;
    [SerializeField]
    string propName;
    public bool beingMoved;
    public delegate void InteractHover(bool hoverstate, string text = "", Transform transform = null);
    public static event InteractHover OnHover;
    public delegate void InteractRequest(Transform objectToInteract);
    public static event InteractRequest OnInteractRequest;
    public string getHoverName()
    {
        return propName;
    }
    #endregion
    #region MonobehaviourDefaults
    public void OnInteract()
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
        if (!MainCanvas.mainCanvas.isUIOverride && beingMoved == false)
        {
            OnHover.Invoke(true, propName, this.transform);
        }

    }
    private void OnMouseExit()
    {
            OnHover.Invoke(false);
    }
    #endregion
}
