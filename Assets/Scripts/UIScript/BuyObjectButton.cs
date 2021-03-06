﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyObjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image rendImage;
    [SerializeField]
    BuyObjectScriptable buyObjectScriptable;
    [SerializeField]
    TextMeshProUGUI buttonText;
    [SerializeField]
    GameObject playerEconomyWarning;

    public BuyObjectScriptable BuyObjectScriptable { get => buyObjectScriptable; set => buyObjectScriptable = value; }

    public delegate void Hover(BuyObjectScriptable gameObject);
    public static event Hover OnHover;
    public delegate void ClickedBuyObject(BuyObject gameObject);
    public static event ClickedBuyObject OnObjectClicked;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = BuyObjectScriptable.objectName + " - Hinta: " + BuyObjectScriptable.objectValue + "e";
    }

    public void BuyObject()
    {
        BuyObject buyObject = new BuyObject(BuyObjectScriptable);
        if (PlayerDataHolder.Current.PlayerMoney.getValue<float>() >= buyObject.BuyValue)
        {
            OnObjectClicked.Invoke(buyObject);
        }
        else
        {
            MainCanvas.mainCanvas.createEconomyWarning();
        }


    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        rendImage.gameObject.SetActive(true);
        OnHover?.Invoke(BuyObjectScriptable);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        rendImage.gameObject.SetActive(false);
    }
}
