using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyObjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    RawImage rendImage;
    [SerializeField]
    BuyObjectScriptable buyObjectScriptable;
    public delegate void Hover(BuyObjectScriptable gameObject);
    public static event Hover OnHover;
    public delegate void ClickedBuyObject(BuyObject gameObject);
    public static event ClickedBuyObject OnObjectClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyObject()
    {
        BuyObject buyObject = new BuyObject(buyObjectScriptable);
        OnObjectClicked.Invoke(buyObject);

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        rendImage.gameObject.SetActive(true);
        OnHover.Invoke(buyObjectScriptable);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        rendImage.gameObject.SetActive(false);
    }
}
