using UnityEngine;
using System.Collections;

public class BuyObject
{
    BuyObjectScriptable buyObjectScriptable;
    string buyName;
    float buyValue;
    float satisfactionGain;
    public BuyObject(BuyObjectScriptable objectScriptable)
    {
        buyObjectScriptable = objectScriptable;
        BuyName = buyObjectScriptable.objectName;
        BuyValue = buyObjectScriptable.objectValue;
        SatisfactionGain = buyObjectScriptable.satisfactionGain;
    }

    public string BuyName { get => buyName; set => buyName = value; }
    public float BuyValue { get => buyValue; set => buyValue = value; }
    public float SatisfactionGain { get => satisfactionGain; set => satisfactionGain = value; }

    public BuyObjectScriptable GetBuyObjectScriptable()
    {
        return buyObjectScriptable;
    }


}
