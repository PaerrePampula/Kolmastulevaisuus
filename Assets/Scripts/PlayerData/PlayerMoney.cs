﻿using UnityEngine;
using System.Collections;

public class PlayerMoney : IStattable
{
    
    float amount = 0;
    public bool UniqueStat { get { return true; } }
    public StatType ThisStatType { get { return StatType.PlayerMoney; } }
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
    }
}