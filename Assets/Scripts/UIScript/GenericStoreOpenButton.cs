using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStoreOpenButton : UIGenerator
{
    [SerializeField]
    List<BuyObjectScriptable> buyables = new List<BuyObjectScriptable>();

    public void onClickSetBuyables()
    {
        getInstantiated().GetComponent<BuyMenu>().Initialize(buyables);
    }

}
