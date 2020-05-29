using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGeneric : MonoBehaviour
{
    public delegate void UIOpen(bool moveStatus);
    public static event UIOpen OnUIOpened;
    public string OnClickResourceToCall; //Esim shortcuttien painamisesta nousevat elementit canvakselle.
    public Transform OnClickResourceParent;
    public void CloseThisUIObject()
    {
        Destroy(gameObject);
        OnUIOpened?.Invoke(true);
    }
    private void Start()
    {
        OnUIOpened?.Invoke(false);
    }
    public void CallElement()
    {
        UiElementCall uiCall = new UiElementCall();
        uiCall.elementToCall = Resources.Load<GameObject>(OnClickResourceToCall);
        uiCall.elementParent = OnClickResourceParent;
        GameEventSystem.Current.DoEvent(
            Event_Type.UI_ELEMENT_CALL,
            uiCall
            );
    }
}
