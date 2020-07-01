using UnityEngine;
using System.Collections;

public class HungerSystem : MonoBehaviour
{

    private void OnEnable()
    {
        LocationHandler.OnLocationChange += incrementHunger;
        GameEventSystem.RegisterListener(Event_Type.SIMSTAT_CHANGE, incrementHunger);

    }
    private void OnDisable()
    {
        LocationHandler.OnLocationChange -= incrementHunger;
        GameEventSystem.UnRegisterListener(Event_Type.SIMSTAT_CHANGE, incrementHunger);
    }
    void incrementHunger()
    {
        float startvalue = PlayerDataHolder.Current.Foodamount.StatFloat;
        PlayerDataHolder.Current.Foodamount.ChangeStat(-10);
        float endvalue = startvalue - 10;
        if (endvalue < 0)
        {

            PlayerDataHolder.Current.Hunger.ChangeStat(-endvalue);

        }
        else
        {
            PlayerDataHolder.Current.Hunger.ChangeStat(-10);
        }

        checkStarving();
    }
    void incrementHunger(EventInfo info)
    {
        SimStatInfo simStatInfo = (SimStatInfo)info;
        if(simStatInfo.SimStatName == SimStatType.Hunger)
        {
            PlayerDataHolder.Current.Hunger.ChangeStat(simStatInfo.StatChange);
            checkStarving();
        }
    }
    void checkStarving()
    {
        if (PlayerDataHolder.Current.Hunger.StatFloat >= 100)
        {
            PlayerDataHolder.Current.Satisfaction.ChangeStat(-25);
            Flag flag = new Flag("INTENSE_HUNGER", 0, true);
            flag.FireFlag();
            PlayerDataHolder.Current.Hunger.ChangeStat(-30);
        }
    }
    public static void EatFood(float amount)
    {
        PlayerDataHolder.Current.Hunger.ChangeStat(-amount);
    }
}
