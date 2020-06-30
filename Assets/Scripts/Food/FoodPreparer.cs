using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPreparer : MonoBehaviour
{

    [SerializeField]
    int foodUseTimes;
    [SerializeField]
    public float cost;
    [SerializeField]
    float saturation;
    [SerializeField]
    string foodName;
    [SerializeField]
    float increaseAmount;


    public virtual void PrepareFood()
    {
        if (PlayerDataHolder.Current.PlayerMoney.getValue<float>() >= cost)
        {
            PlayerDataHolder.Current.Foodamount.ChangeStat(increaseAmount);

            PlayerEconomy.createPurchase("Ruokakauppareissut", -cost, true);
        }
        else
        {
            MainCanvas.mainCanvas.createEconomyWarning();
        }


    }
}
