using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStoreOpenButton : UIGenerator
{
    [SerializeField]
    List<BuyObjectScriptable> buyables = new List<BuyObjectScriptable>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void onClickSetBuyables()
    {
        getInstantiated().GetComponent<BuyMenu>().Initialize(buyables);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
