using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : UiGeneric
{
    [SerializeField]
    Transform storeContentTransform;
    [SerializeField]
    GameObject buyObjectButtonPrefab;

    public void Initialize(List<BuyObjectScriptable> buyObjects)
    {
        for (int i = 0; i < storeContentTransform.childCount; i++)
        {
            Destroy(storeContentTransform.GetChild(i).gameObject);
        }

        transform.localPosition = new Vector3(-700, 0, 0);

        for (int i = 0; i < buyObjects.Count; i++)
        {
            GameObject go = Instantiate(buyObjectButtonPrefab, storeContentTransform);
            go.GetComponent<BuyObjectButton>().BuyObjectScriptable = buyObjects[i];
        }
    }

}
