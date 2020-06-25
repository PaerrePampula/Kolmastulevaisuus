using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShopping : MonoBehaviour
{
    [SerializeField]
    GameObject foodCircleUI;
    public void CreateMenu()
    {
        GameObject go = Instantiate(foodCircleUI);
        go.transform.SetParent(MainCanvas.mainCanvas.parentOfNewTransforms);
        go.transform.localPosition = Vector3.zero;
    }
}
