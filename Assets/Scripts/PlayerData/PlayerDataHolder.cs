using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PlayerDataHolder
{
    #region TYÖT
    private static Job playerJob;
    #endregion

    #region RAHA, TALOUS

    private static PlayerMoney playerMoney = new PlayerMoney();
    private static List<IncomeSource> incomeSources = new List<IncomeSource>();
    public static List<ListableExpense> MonthlyListableExpenses = new List<ListableExpense>();
    public static List<ListableExpense> OtherListableExpenses = new List<ListableExpense>(); //Kaikki epäsäännölliset kulut
    #endregion

    #region ASUNTO, VUOKRA
    private static Rent rent;
    private static RentableHome rentablehome;
    #endregion
    #region Statsit

    #endregion
    public static Rent PlayerRent
    {
        get
        {

            if (rentablehome == null)
            {
                RentableHome newrentableHome = new RentableHome(Resources.Load<RentableHomeScriptable>("FallBack"));
                rentablehome = newrentableHome;
                rent = new Rent(rentablehome.BaseRent, rentablehome.WaterCost, rentablehome.ElectricityCost);
                return rent;
                //Ei tarvii käydä mainmenun kautta jos haluu testata jotain tämän avulla
            }
            if (rent == null)
            {
                rent = new Rent(rentablehome.BaseRent, rentablehome.WaterCost, rentablehome.ElectricityCost);
            }
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
    public static PlayerMoney PlayerMoney
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
    public static Job PlayerJob
    {
        get
        {
            return playerJob;
        }
        set
        {
            playerJob = value;
        }
    }
    [RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        DateTimeSystem.OnMonthChange += clearMonthBudget;
    }
    static void clearMonthBudget()
    {
        OtherListableExpenses.Clear();
    }
}
