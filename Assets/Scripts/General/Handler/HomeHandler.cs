using UnityEngine;

public static class HomeHandler
{
    #region Fields
    static Rent playerRent;
    static RentableHome playerHome;
    static HomeHandler()
    {
        DateTimeSystem.OnMonthChange += PayRent;
    }
    #endregion

    static void PayRent()
    {
        PaerToolBox.changePlayerMoney(-PlayerDataHolder.PlayerRent.getTotal());
    }

}
