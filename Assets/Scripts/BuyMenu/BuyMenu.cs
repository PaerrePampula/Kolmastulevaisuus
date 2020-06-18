using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : UiGeneric
{
    [SerializeField]
    Transform storeContentTransform;
    [SerializeField]
    GameObject buyObjectButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
