using UnityEngine;
using System.Collections;

public class BuyObject
{
    BuyObjectScriptable buyObjectScriptable;
    string buyName;
    float buyValue;
    public BuyObject(BuyObjectScriptable objectScriptable)
    {
        buyObjectScriptable = objectScriptable;
        BuyName = buyObjectScriptable.objectName;
        BuyValue = buyObjectScriptable.objectValue;
    }

    public string BuyName { get => buyName; set => buyName = value; }
    public float BuyValue { get => buyValue; set => buyValue = value; }

    public BuyObjectScriptable GetBuyObjectScriptable()
    {
        return buyObjectScriptable;
    }


}
