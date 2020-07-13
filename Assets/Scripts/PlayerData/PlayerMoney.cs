using UnityEngine;
using System.Collections;

public class PlayerMoney : IStattable
{
    
    float amount;
    public bool UniqueStat { get { return true; } }
    public StatType ThisStatType { get { return StatType.PlayerMoney; } }
    public delegate void NewMoneyChange(float moneyAmount);
    public event NewMoneyChange onMoneyChange;
    public PlayerMoney()
    {
        StatsChecker.RegisterStat(this);
    }
    public T getValue<T>()
    {
        return (T)(object)amount;
    }
    public void MoneyChange(float Chgamount)
    {
        amount += Chgamount;
        onMoneyChange.Invoke(amount);
    }
    public void resetMoney()
    {
        amount = 0;
        onMoneyChange.Invoke(amount);
    }
}
