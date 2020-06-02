using UnityEngine;
using UnityEngine.EventSystems;

public class MainCanvas : MonoBehaviour
{
    #region Fields
    public bool isUIOverride { get; private set; } //Tätä käytetään, kun ei haluta että UIn läpi pystyy painamaan asioita.
    static Transform canvasTransform; //Main canvaksen transform, hyödyllinen esim UI elementtien parentoimisessa.
    static private MainCanvas _currentMainCanvas; //Tämänhetkinen pääcanvas.
    static public MainCanvas mainCanvas //getataan maincanvas ja setataan se jos se on null
    {
        get
        {
            if (_currentMainCanvas == null)
            {
                _currentMainCanvas = FindObjectOfType<MainCanvas>();
            }
            return _currentMainCanvas;
        }
    }
    #endregion
    private void OnDestroy() //Periaatteessa mainmenulla on oma maincanvas, ja niin on scenellä, kaikki menee sekaisin jos sceneloadin jälkeen jää vielä mainmenun "kuolleen" canvaksen listeneri, kutsuu ui-elementtejä haudan takaa
    {
        GameEventSystem.Current.UnRegisterListener(Event_Type.UI_ELEMENT_CALL, callNewUI);
    }
    #region Getters
    public static Transform getMainCanvasTransform() //Muitten classien käyttöön getteri pääcanvaksesta. Maincanvas static, joten niitä on scenessä vain yksi.
    {
        return canvasTransform;
    }
    #endregion

    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = this.transform;
        GameEventSystem.Current.RegisterListener(Event_Type.UI_ELEMENT_CALL, callNewUI);
    }

    // Update is called once per frame
    void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
    }
    #endregion



    void callNewUI(EventInfo info) //käytetään uusien UI elementtien tuomiseen ruudulle. UIGeneric skriptin alta löytyy käyttö.
    {
        UiElementCall uiElementCall = (UiElementCall)info;
        GameObject calledObject = Instantiate(uiElementCall.elementToCall);
        calledObject.transform.SetParent(uiElementCall.elementParent);
        calledObject.transform.localPosition = Vector3.zero;
    }
}
