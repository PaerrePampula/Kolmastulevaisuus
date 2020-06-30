using UnityEngine;

public class UiGeneric : MonoBehaviour //Jokaisen UI palikkaan sopivia ominaisuuksia, olisi voinut olla myös ui-elementeissä inherit tyylisesti, mutta ajattelin että komponenttilähestymistapa pitäisi asiat yksinkertaisina.
{
    #region Fields
    public delegate void UIOpen(bool moveStatus);
    public static event UIOpen OnUIOpened;
    public GameObject OnClickResourceToCall; //Esim shortcuttien painamisesta nousevat elementit canvakselle.
    public Transform OnClickResourceParent;
    #endregion
    #region MonobehaviourDefaults
    private void Start()
    {
        OnUIOpened?.Invoke(false); //Jumittaa siis pelaajan liikkeen
    }
    #endregion
    public void CloseThisUIObject()
    {
        OnUIOpened?.Invoke(true); //Päästää pelaajaan taas liikkeelle
        Destroy(gameObject);

    }
    public void DestroyAbove()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }


    public void CallElement() //Kutsutaan ui-elementti x peliin jossakin click eventissä esim.
    {

        UiElementCall uiCall = new UiElementCall();
        uiCall.elementToCall = OnClickResourceToCall;
        uiCall.elementParent = OnClickResourceParent;
        GameEventSystem.DoEvent(
            Event_Type.UI_ELEMENT_CALL,
            uiCall
            );
    }
}
