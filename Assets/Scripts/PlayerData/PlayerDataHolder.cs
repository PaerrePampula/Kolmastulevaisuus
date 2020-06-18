using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDataHolder : MonoBehaviour
{
    static private PlayerDataHolder _Current;
    static public PlayerDataHolder Current
    {
        get
        {
            if (_Current == null)
            {
                _Current = FindObjectOfType<PlayerDataHolder>();
            }
            return _Current;
        }
    }
    #region TYÖT
    private Job playerJob;
    #endregion

    #region RAHA, TALOUS

    private PlayerMoney playerMoney;
    private List<IncomeSource> incomeSources;
    public  List<ListableExpense> MonthlyListableExpenses = new List<ListableExpense>();
    public  List<ListableExpense> OtherListableExpenses = new List<ListableExpense>(); //Kaikki epäsäännölliset kulut
    #endregion

    #region ASUNTO, VUOKRA
    private Rent rent;
    private static RentableHome rentablehome;
    #endregion
    #region Statsit
    private Stat satisfaction;
    private Stat comfortableness;
    private Stat hunger;
    #endregion
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
    public  Rent PlayerRent
    {
        get
        {

            if (playerHome == null)
            {
                RentableHome newrentableHome = new RentableHome(Resources.Load<RentableHomeScriptable>("FallBack"));
                playerHome = newrentableHome;
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

    public PlayerMoney PlayerMoney
    {
        get
        {
            if (playerMoney == null)
            {
                playerMoney = new PlayerMoney();
            }
            return playerMoney;
        }
        set
        {
            playerMoney = value;
        }
    }
    public List<IncomeSource> IncomeSources
    {
        get
        {
            if (incomeSources == null)
            {
                incomeSources = new List<IncomeSource>();
            }
            return incomeSources;
        }
        set
        {
            incomeSources = value;
        }
    }
    public Job PlayerJob
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

    public Stat Satisfaction
    {
        get 
        { 
            if (satisfaction == null)
            {
               satisfaction  = new Stat(SimStatType.Satisfaction);
            }
            return satisfaction; 
        }
        set
        {
            satisfaction = value;
        }
    }

    public Stat Comfortableness
    {
        get 
        { 
            if (comfortableness == null)
            {
                comfortableness = new Stat(SimStatType.Comfortableness);
            }
            return comfortableness; 
        }
        set
        {
            comfortableness = value;
        }
    }

    public Stat Hunger
    {
        get 
        {
            if (hunger == null)
            {
                hunger  = new Stat(SimStatType.Hunger);
            }
            return hunger; 
        }
        set
        {
            hunger = value;
        }
    }

    void OnEnable()
    {
        DateTimeSystem.OnMonthChange += clearMonthBudget;
        playerMoney = new PlayerMoney();

        Satisfaction.Init();
        Comfortableness.Init();
    }

    void clearMonthBudget()
    {
        OtherListableExpenses.Clear();
    }
}
