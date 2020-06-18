using System;

public class Rent : IStattable
{
    #region Fields
    float rent;
    float waterCost;
    float electricityCost;
    public bool UniqueStat { get { return true; } }
    public StatType ThisStatType { get { return StatType.Rent; } }
    #endregion
    #region constructors
    public Rent(float rentamount, float watercost =0, float eleccost = 0)
    {
        rent = rentamount;
        waterCost = watercost;
        electricityCost = eleccost;
        ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.Current.MonthlyListableExpenses, ("Vuokra", rent));
        if (watercost != 0)
        {
            ListableExpense listableWater = new ListableExpense(PlayerDataHolder.Current.MonthlyListableExpenses, ("Vesimaksu", waterCost));
        }
        if (eleccost != 0)
        {
            ListableExpense listableElectricity = new ListableExpense(PlayerDataHolder.Current.MonthlyListableExpenses, ("Sähkölasku", electricityCost));
        }
    }

    #endregion
    #region getters
    public float getTotal()
    {
        return rent + waterCost + electricityCost;
    }

    public T getValue<T>()
    {
        return (T)(object)getTotal();
    }

    #endregion
}
