using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPreparer : MonoBehaviour
{
    public delegate void FoodPrepare(FoodItem food);
    public static event FoodPrepare onFoodPrepare;
    [SerializeField]
    int foodUseTimes;
    [SerializeField]
    float cost;
    [SerializeField]
    float saturation;
    [SerializeField]
    string foodName;


    public void PrepareFood()
    {
        FoodItem food = new FoodItem(foodName, foodUseTimes, saturation);
        onFoodPrepare?.Invoke(food);
        PlayerEconomy.createPurchase(foodName, -cost, true);

    }
}
