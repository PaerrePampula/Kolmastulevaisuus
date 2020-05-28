using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGeneric : MonoBehaviour
{
    public delegate void UIOpen(bool moveStatus);
    public static event UIOpen OnUIOpened;
    public void CloseThisUIObject()
    {
        Destroy(gameObject);
        OnUIOpened?.Invoke(true);
    }
    private void Start()
    {
        OnUIOpened?.Invoke(false);
    }

}
