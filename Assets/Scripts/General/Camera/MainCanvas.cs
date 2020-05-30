using UnityEngine;
using UnityEngine.EventSystems;

public class MainCanvas : MonoBehaviour
{
    #region Fields
    public bool isUIOverride { get; private set; }
    static Transform canvasTransform;
    static private MainCanvas _currentMainCanvas;
    static public MainCanvas mainCanvas
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

    #region Getters
    public static Transform getMainCanvasTransform()
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



    void callNewUI(EventInfo info)
    {
        UiElementCall uiElementCall = (UiElementCall)info;
        GameObject calledObject = Instantiate(uiElementCall.elementToCall);
        calledObject.transform.SetParent(uiElementCall.elementParent);
        calledObject.transform.localPosition = Vector3.zero;
    }
}
