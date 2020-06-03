﻿using System;
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
    #endregion

    #region ASUNTO, VUOKRA
    private static Rent rent;
    private static RentableHome rentablehome;
    #endregion
    static PlayerDataHolder()
    {

    }
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
}