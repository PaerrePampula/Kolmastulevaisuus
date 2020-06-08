using UnityEngine;
using UnityEngine.EventSystems;

public class MainCanvas : SceneCanvas
{
    #region Fields
    public bool isUIOverride { get; private set; } //Tätä käytetään, kun ei haluta että UIn läpi pystyy painamaan asioita.
    static Transform canvasTransform; //Main canvaksen transform, hyödyllinen esim UI elementtien parentoimisessa.
    public Transform parentOfNewTransforms;
    public delegate void Freezer(bool status);
    public static event Freezer OnFreeze;
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


    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {

        canvasTransform = parentOfNewTransforms;
        GameEventSystem.RegisterListener(Event_Type.UI_ELEMENT_CALL, callNewUI);
    }

    // Update is called once per frame
    void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
        shouldPlayerBeFrozen(); //Ei kamalasti overheadiä, varmaan ihan ok näin.
    }
    #endregion


    
    void callNewUI(EventInfo info) //käytetään uusien UI elementtien tuomiseen ruudulle. UIGeneric skriptin alta löytyy käyttö.
    {
        UiElementCall uiElementCall = (UiElementCall)info;
        GameObject calledObject = Instantiate(uiElementCall.elementToCall);
        calledObject.transform.SetParent(uiElementCall.elementParent);
        calledObject.transform.localPosition = Vector3.zero;
    }
    void shouldPlayerBeFrozen()
    {
        if (parentOfNewTransforms.childCount > 0)
        {
            OnFreeze?.Invoke(false);
        }
        if (parentOfNewTransforms.childCount == 0)
        {
            OnFreeze?.Invoke(true);
        }

    }

    public override Transform getMainCanvasTransform()
    {
        return parentOfNewTransforms;
    }
}
