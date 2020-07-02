using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuInstructionTooltips : MonoBehaviour
{
    [SerializeField]
    Transform tooltipTransform;
    private void OnEnable()
    {
        PlacementHelper.OnPlacementInteract += interact;
    }
    private void OnDisable()
    {
        PlacementHelper.OnPlacementInteract -= interact;
    }

    void interact(bool isBegin, bool isObjectChange = false)
    {
        switch (isBegin)
        {
            case true:
                tooltipTransform.gameObject.SetActive(true);
                break;
            case false:
                tooltipTransform.gameObject.SetActive(false);
                break;

        }
    }
}
