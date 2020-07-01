﻿using UnityEngine;

public static class HomeHandler
{
    #region Fields
    static Rent playerRent;
    static RentableHome playerHome;
    [RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.BeforeSceneLoad)] //Ei runaa ilman tätä koska ei monobehaviouria.
    static void Init()
    {
        DateTimeSystem.OnMonthChange += PayRent;
        ResetButton.onReset += UnInit;
    }
    static void UnInit()
    {
        DateTimeSystem.OnMonthChange -= PayRent;
        ResetButton.onReset -= UnInit;
    }
    #endregion
    static void PayRent()
    {
        PaerToolBox.changePlayerMoney(-PlayerDataHolder.PlayerRent.getTotal());
    }

}
