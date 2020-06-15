using UnityEngine;
using System.Collections;

public class BuyObject
{
    BuyObjectScriptable buyObjectScriptable;
    public BuyObject(BuyObjectScriptable objectScriptable)
    {
        buyObjectScriptable = objectScriptable;
    }

    public BuyObjectScriptable GetBuyObjectScriptable()
    {
        return buyObjectScriptable;
    }


}
