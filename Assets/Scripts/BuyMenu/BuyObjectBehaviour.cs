using UnityEngine;
using System.Collections;

public class BuyObjectBehaviour : MonoBehaviour
{
    float buyValue;
    float satisfactionGain;
    public void Initialize(float buyObjectValue, float buyObjectSatisfaction)
    {
        buyValue = buyObjectValue;
        satisfactionGain = buyObjectSatisfaction;
    }
    public void SellItem()
    {
        PaerToolBox.changePlayerMoney(buyValue*0.75f);
        PlayerDataHolder.Current.Comfortableness.ChangeStat(-satisfactionGain);
    }
}
