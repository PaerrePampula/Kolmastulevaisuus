using UnityEngine;

public class UiGeneric : MonoBehaviour
{
    #region Fields
    public delegate void UIOpen(bool moveStatus);
    public static event UIOpen OnUIOpened;
    public string OnClickResourceToCall; //Esim shortcuttien painamisesta nousevat elementit canvakselle.
    public Transform OnClickResourceParent;
    #endregion
    #region MonobehaviourDefaults
    private void Start()
    {
        OnUIOpened?.Invoke(false);
    }
    #endregion
    public void CloseThisUIObject()
    {
        Destroy(gameObject);
        OnUIOpened?.Invoke(true);
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
