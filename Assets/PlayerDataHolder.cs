using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PlayerDataHolder
{

    #region RAHA, TALOUS
    private static float playerMoney;
    private static List<IncomeSource> incomeSources = new List<IncomeSource>();
    #endregion

    #region ASUNTO, VUOKRA
    private static Rent rent;
    private static RentableHome rentablehome;
    #endregion

    public static Rent PlayerRent
    {
        get
        {

            if (rentablehome == null)
            {
                RentableHome rentableHome = new RentableHome(Resources.Load<RentableHomeScriptable>("FallBack"));
                rent = new Rent(rentableHome.getRentTotalForAMonth());
                return rent;
                //Ei tarvii käydä mainmenun kautta jos haluu testata jotain tämän avulla
            }
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
    public static List<IncomeSource> IncomeSources
    {
        get
        {
            return incomeSources;
        }
        set
        {
            incomeSources = value;
        }
    }
}
