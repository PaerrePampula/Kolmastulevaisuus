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
    public List<ListableExpense> MonthlyListableExpenses = new List<ListableExpense>();
    public List<ListableExpense> OtherListableExpenses = new List<ListableExpense>(); //Kaikki epäsäännölliset kulut

    #endregion
    #region META
    int limitedUseWorldInteractableStamina = 2;
    #endregion
    #region ASUNTO, VUOKRA
    public List<FoodItem> playerFoods = new List<FoodItem>();
    private Rent rent;
    private static RentableHome rentablehome;
    #endregion
    #region Statsit
    private Stat satisfaction;
    private Stat comfortableness;
    private Stat hunger;
    private Stat ranking;
    List<Stat> allStats;
    Stat social;
    Stat study;
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
    public Rent PlayerRent
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
                satisfaction = new Stat(SimStatType.Satisfaction, 0, 100);
                allStats.Add(satisfaction);
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
                comfortableness = new Stat(SimStatType.Comfortableness, 0, 100);
                allStats.Add(comfortableness);

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
                hunger = new Stat(SimStatType.Hunger, 0, 100);
                allStats.Add(hunger);

            }
            return hunger;
        }
        set
        {
            hunger = value;
        }
    }

    public Stat Ranking
    {
        get
        {
            if (ranking == null)
            {
                ranking = new Stat(SimStatType.Ranking, -5000, 5000);
                allStats.Add(ranking);

            }
            return ranking;
        }
        set
        {

            ranking = value;
        }
    }

    public int LimitedUseWorldInteractableStamina
    {
        get { return limitedUseWorldInteractableStamina; }
        set
        {
            limitedUseWorldInteractableStamina = value;
        }
    }

    public Stat Social
    {
        get 
        {
            if (social == null)
            {
                social = new Stat(SimStatType.Social,0,100);
                allStats.Add(social);
            }
            return social; 
        }
        set
        {
            social = value;
        }
    }
    public Stat Study
    {
        get
        {
            if (study == null)
            {
                study = new Stat(SimStatType.Study, 0, 100);
                allStats.Add(study);
            }
            return study;
        }

        set
        {
            study = value;
        }
    }

    void OnEnable()
    {
        allStats = new List<Stat>();
        playerMoney = new PlayerMoney();

        Satisfaction.Init();
        Comfortableness.Init();
        Ranking.Init();
        Hunger.Init();
        Social.Init();
        Study.Init();

        FoodItem.onFoodExpire += removeExpiredFood;
        WorldLimitedUseInteractable.onInteractUse += deductStamina;
        LocationHandler.OnTurnEnd += resetStamina;
        FoodPreparer.onFoodPrepare += ListFood;
        DateTimeSystem.OnMonthChange += clearMonthBudget;
    }
    private void OnDisable()
    {
        FoodItem.onFoodExpire -= removeExpiredFood;
        WorldLimitedUseInteractable.onInteractUse -= deductStamina;
        LocationHandler.OnTurnEnd -= resetStamina;
        FoodPreparer.onFoodPrepare -= ListFood;
        DateTimeSystem.OnMonthChange -= clearMonthBudget;
    }

    void clearMonthBudget()
    {
        OtherListableExpenses.Clear();
    }
    void removeExpiredFood(FoodItem food)
    {
        playerFoods.Remove(food);
        Debug.Log("Food remove");
    }
    void deductStamina()
    {
        LimitedUseWorldInteractableStamina--;
    }
    void resetStamina()
    {
        LimitedUseWorldInteractableStamina = 2;
    }
    void ListFood(FoodItem food)
    {
        playerFoods.Add(food);
    }
    public Stat getStatByEnum(SimStatType simStatType)
    {
        var stat = allStats.SingleOrDefault(givenStat => givenStat.SimStatType == simStatType);
        return stat;
    }
}
