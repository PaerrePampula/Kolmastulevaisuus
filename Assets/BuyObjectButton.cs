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
    GameObject buyObjectPrefab;
    public delegate void Hover(GameObject gameObject);
    public static event Hover OnHover;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        rendImage.gameObject.SetActive(true);
        OnHover.Invoke(buyObjectPrefab);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        rendImage.gameObject.SetActive(false);
    }
}
