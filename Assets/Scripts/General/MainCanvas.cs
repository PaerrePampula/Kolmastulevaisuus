using UnityEngine;
using UnityEngine.EventSystems;

public class MainCanvas : SceneCanvas
{
    #region Fields
    public bool isUIOverride { get; private set; } //Tätä käytetään, kun ei haluta että UIn läpi pystyy painamaan asioita.
    static Transform canvasTransform; //Main canvaksen transform, hyödyllinen esim UI elementtien parentoimisessa.
    public Transform parentOfNewTransforms;
    public Transform parentofGameEvents;
    [SerializeField]
    GameObject playerEconomyWarning;
    public delegate void Freezer(bool status);
    public static event Freezer OnFreeze;
    public bool freezeOverride;
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
        RandomEventUI.newEventTriggered += checkEventUIEnable;
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
        calledObject.transform.localScale = new Vector3(1, 1, 1);
    }
    void shouldPlayerBeFrozen()
    {
        if ((parentofGameEvents.childCount >= 0))
        {
            OnFreeze?.Invoke(false);
        }
        if ((parentOfNewTransforms.childCount <= 1) && (parentofGameEvents.childCount == 0) && (freezeOverride == false))
        {
            OnFreeze?.Invoke(true);
        }

    }

    public override Transform getMainCanvasTransform(bool isEvent = false)
    {
        if (isEvent) return parentofGameEvents;
        else return parentOfNewTransforms;

    }
    void checkEventUIEnable(int index)
    {
        try
        {
            if (parentofGameEvents.GetChild(index).gameObject.activeSelf != true)
            {
                parentofGameEvents.GetChild(index).gameObject.SetActive(true);
            }
        }
        catch (UnityException)
        {
            return;
        }

    }
    public void createEconomyWarning()
    {
        GameObject go = Instantiate(playerEconomyWarning);
        go.gameObject.transform.SetParent(MainCanvas.mainCanvas.transform);
        go.transform.localPosition = Vector3.zero;
    }
}
