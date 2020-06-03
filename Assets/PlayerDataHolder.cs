using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PlayerDataHolder
{
    private static float playerMoney;
    private static Rent rent;
    private static RentableHome rentablehome;
    public static Rent PlayerRent
    {
        get
        {
            rent = new Rent(rentablehome.getRentTotalForAMonth());
            return rent;
        }
        set
        {
            rent = value;
        }
    }
    public static RentableHome playerHome
    {
        get
        {
            return rentablehome;
        }
        set
        {
            rentablehome = value;
        }
    }
    public static float PlayerMoney
    {
        get
        {
            return playerMoney;
        }
        set
        {
            playerMoney = value;
        }
    }
}
