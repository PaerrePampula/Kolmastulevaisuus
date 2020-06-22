using UnityEngine;
using System.Collections;

public class FoodItem : IButtonInteractableItem
{
    int timesOfUse;
    System.DateTime useBefore;
    string foodName;
    float saturation;
    public int TimesOfUse
    {
        get 
        {
            if (timesOfUse == 0)
            {
                onFoodExpire?.Invoke(this);
            }
            return timesOfUse; 
        }
        set
        {

            timesOfUse = value;
        }
    }

    public delegate void ExpireFoodCheck(FoodItem foodItem);
    public static event ExpireFoodCheck onFoodExpire;

    public FoodItem(string newFoodName, int timesOfUse, float saturationAmount)
    {
        foodName = newFoodName;
        TimesOfUse = timesOfUse;
        saturation = saturationAmount;
        useBefore = (DateTimeSystem.getCurrentDate().AddDays(21));
        LocationHandler.OnTurnEnd += checkItemUsability;
    }

    bool useCheck()
    {
        return ((TimesOfUse > 0)) && (useBefore > DateTimeSystem.getCurrentDate()) ? true : false;
    }
    public void useFoodItem()
    {
        if (useCheck() == true)
        {
            HungerSystem.EatFood(saturation);
            TimesOfUse--;
        }
        else
        {
            onFoodExpire?.Invoke(this);
        }

    }

    void checkItemUsability()
    {
        if (useCheck() != true)
        {
            onFoodExpire.Invoke(this);
            LocationHandler.OnTurnEnd -= checkItemUsability;
        }
    }
    public void InteractWithItem()
    {
        useFoodItem();
    }
    public override string ToString()
    {
        return foodName + "\nannoksia jäljellä: " + TimesOfUse;
    }
}
