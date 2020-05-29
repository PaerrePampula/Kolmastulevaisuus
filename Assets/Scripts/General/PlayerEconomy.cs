﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    #region Fieldit
    //Tämä on prototyyppi lopullisesta pelaajan taloudenlaskemisen simuloinnista, toistaiseksi melko yksinkertainen
    private float playerMoney; //Yksiselitteisesti pelaajan rahatilanne.
    private List<IncomeSource> incomeSources = new List<IncomeSource>();
    public delegate void IncreaseAction(float amount);
    public static event IncreaseAction OnIncrease;
    //Delegatesta ja eventistä. Näillä kukkaro saa lähetettyä rahanmuutoksesta viestin kaikille onincreasen tilanneille 
    //classeille viestin siitä, että rahatilanne on muuttunut, delegaten ja eventin avulla näitä tilaajia ei tarvitse
    //erikseen unityn editorissä määritellä, eli toisinsanoen pelaajan kukkaron ei tarvitse tietää, kenelle tätä viestiä lähetetään.

    private static PlayerEconomy _playerEconomy;
    static public PlayerEconomy CurrentPlayerEconomy
    {
        get
        {
            if (_playerEconomy == null)
            {
                _playerEconomy = FindObjectOfType<PlayerEconomy>();
            }
            return _playerEconomy;
        }
    }
    #endregion

    #region Raha setterit ja getterit
    public void SetMoney(EventInfo info)
    {
        FloatChangeInfo floatChangeInfo = (FloatChangeInfo)info;
        playerMoney += floatChangeInfo.changeofFloat;
        PaerToolBox.callOnStatChange(StatType.PlayerMoney, playerMoney.ToString(), true);
        OnIncrease?.Invoke(playerMoney);
    }
    public void GetMoney()
    {
        Debug.Log(playerMoney);
    }


    public List<IncomeSource> GetIncomeSources()
    {
        return incomeSources;
    }
    public float getAllIncomeSourceGrossTotals(int monthAmount)
    {
        float total = 0;
        for (int i = 0; i < incomeSources.Count; i++)
        {
            total += (incomeSources[i].getGrossIncomeAmountInAMonth() * monthAmount);
        }
        return total;
    }
    #endregion


    void Start()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.FLOAT_CHANGE, SetMoney);
        IncomeSource incomeSource = new IncomeSource(2666);
        incomeSources.Add(incomeSource);
        TaxationSystem.taxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));
        Debug.Log(TaxationSystem.taxationSystem.getIncomeAfterTaxes(getAllIncomeSourceGrossTotals(12)));
    }
    private void OnEnable()
    {
        DateTimeSystem.OnMonthChange += PayFromIncomeSources;
    }
    private void OnDisable()
    {
        DateTimeSystem.OnMonthChange -= PayFromIncomeSources;
    }
    void PayFromIncomeSources()
    {
        for (int i = 0; i < incomeSources.Count; i++)
        {
            PaerToolBox.giveMoneyToPlayer(incomeSources[i].getNetIncomeInAMonth());
        }
    }

}