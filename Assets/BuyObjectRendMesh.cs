﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyObjectRendMesh : MonoBehaviour
{
    GameObject displayObject;
    private void OnEnable()
    {
        BuyObjectButton.OnHover += instantiateNewHoverObject;
    }
    private void OnDisable()
    {
        BuyObjectButton.OnHover -= instantiateNewHoverObject;
    }
    void instantiateNewHoverObject(BuyObjectScriptable newHover)
    {
        if (displayObject != null)
        {
            Destroy(displayObject);
        }
        displayObject = Instantiate(newHover.prefab, transform);
        displayObject.transform.localPosition = Vector3.zero;
        displayObject.transform.rotation = Quaternion.identity;

    }
}