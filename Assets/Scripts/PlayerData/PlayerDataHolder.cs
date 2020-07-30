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
    private List<Incomeable> incomeSources;
    public static List<ListableExpense> MonthlyListableExpenses = new List<ListableExpense>();
    public static List<ListableExpense> OtherListableExpenses = new List<ListableExpense>(); //Kaikki epäsäännölliset kulut

    #endregion
    #region META
    private Stat stamina;
    #endregion
    #region ASUNTO, VUOKRA

    private static Rent rent;
    private static RentableHome rentablehome;
    #endregion
    #region Statsit
    private Stat satisfaction;
    private Stat comfortableness;
    private Stat hunger;
    private Stat generalRanking;
    private Stat homeRanking;
    private Stat socialRanking;
    private Stat studyRanking;

    Stat foodamount;
    List<Stat> allStats;
    Stat social;
    Stat study;
    #endregion
    public static RentableHome playerHome
    {
        get
        {
            if (rentablehome == null)
            {
                RentableHome newrentableHome = new RentableHome(Resources.Load<RentableHomeScriptable>("FallBack"));
                rentablehome = newrentableHome;
                PlayerRent = new Rent(rentablehome.BaseRent, rentablehome.WaterCost, rentablehome.ElectricityCost);
            }
            return rentablehome;
        }
        set
        {
            rentablehome = value;
            PlayerRent = new Rent(value.BaseRent, value.WaterCost, value.ElectricityCost);
            for (int i = 0; i < rentablehome.RentExtras.Length; i++)
            {
                Bill bill = new Bill(rentablehome.RentExtras[i].extraName, rentablehome.RentExtras[i].extraCostPerMonth);
            }

        }
    }
    public static Rent PlayerRent
    {
        get
        {

            if (playerHome == null)
            {
                RentableHome newrentableHome = new RentableHome(Resources.Load<RentableHomeScriptable>("FallBack"));
                playerHome = newrentableHome;

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

            return playerMoney;
        }
        set
        {
            playerMoney = value;
        }
    }
    public List<Incomeable> IncomeSources
    {
        get
        {
            if (incomeSources == null)
            {
                incomeSources = new List<Incomeable>();
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

    public Stat GeneralRanking
    {
        get
        {
            if (generalRanking == null)
            {
                generalRanking = new Stat(SimStatType.Ranking, -5000, 5000);
                allStats.Add(generalRanking);

            }
            return generalRanking;
        }
        set
        {

            generalRanking = value;
        }
    }
    public Stat HomeRanking
    {
        get
        {
            if (homeRanking == null)
            {
                homeRanking = new Stat(SimStatType.Ranking, 0, 100);
                allStats.Add(homeRanking);

            }
            return homeRanking;
        }
        set
        {

            homeRanking = value;
        }
    }
    public Stat SocialRanking
    {
        get
        {
            if (socialRanking == null)
            {
                socialRanking = new Stat(SimStatType.Ranking, 0, 100);
                allStats.Add(socialRanking);

            }
            return socialRanking;
        }
        set
        {

            socialRanking = value;
        }
    }
    public Stat StudyRanking
    {
        get
        {
            if (studyRanking == null)
            {
                studyRanking = new Stat(SimStatType.Ranking, 0, 100);
                allStats.Add(studyRanking);

            }
            return studyRanking;
        }
        set
        {

            studyRanking = value;
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

    public Stat Foodamount
    {
        get 
        {
            if (foodamount == null)
            {
                foodamount = new Stat(SimStatType.FoodResources, 0, 100);
                allStats.Add(foodamount);
            }
            return foodamount; 
        }
        set
        {
            foodamount = value;
        }
    }

    public Stat Stamina
    {
        get 
        {
            if(stamina == null)
            {
                stamina = new Stat(SimStatType.Stamina, 0, 100);
                stamina.SetStat(2);
                allStats.Add(stamina);
            }
            return stamina; 
        }
        set
        {
            stamina = value;
        }
    }

    void OnEnable()
    {

        allStats = new List<Stat>();
        playerMoney = new PlayerMoney(true);

        Satisfaction.Init();
        Comfortableness.Init();
        GeneralRanking.Init();
        Hunger.Init();
        Social.Init();
        Study.Init();



        WorldLimitedUseInteractable.onInteractUse += deductStamina;
        LocationHandler.OnTurnEnd += resetStamina;

        DateTimeSystem.OnMonthChange += clearMonthBudget;
    }
    private void OnDisable()
    {

        WorldLimitedUseInteractable.onInteractUse -= deductStamina;
        LocationHandler.OnTurnEnd -= resetStamina;

        DateTimeSystem.OnMonthChange -= clearMonthBudget;
        MonthlyListableExpenses = new List<ListableExpense>();
        OtherListableExpenses = new List<ListableExpense>();

    }

    void clearMonthBudget()
    {
        OtherListableExpenses.Clear();
    }

    void deductStamina()
    {
        Stamina.ChangeStat(-1);
    }
    void resetStamina()
    {
        Stamina.SetStat(2);

    }

    public Stat getStatByEnum(SimStatType simStatType)
    {
        var stat = allStats.SingleOrDefault(givenStat => givenStat.SimStatType == simStatType);
        return stat;
    }
    public float getTotalCosts()
    {
        float result = 0;
        for (int i = 0; i < MonthlyListableExpenses.Count; i++)
        {
            result += MonthlyListableExpenses[i].getTotal();
        }
        return result;
    }
}
