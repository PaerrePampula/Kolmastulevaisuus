using System.Collections.Generic;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    #region Fields
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
    private List<IncomeSource> incomeSources() //Shorthand PlayerDataHolder.IncomeSources
    {
        return PlayerDataHolder.IncomeSources;
    }
    #endregion

    #region Getters and setters
    public void SetMoney(EventInfo info)
    {
        FloatChangeInfo floatChangeInfo = (FloatChangeInfo)info;
        PlayerDataHolder.PlayerMoney += floatChangeInfo.changeofFloat;
        PaerToolBox.callOnStatChange(StatType.PlayerMoney, true, PlayerDataHolder.PlayerMoney.ToString(), PlayerDataHolder.PlayerMoney);
        OnIncrease?.Invoke(PlayerDataHolder.PlayerMoney);
    }
    public void GetMoney() //Debug. poista joskus
    {
        Debug.Log(PlayerDataHolder.PlayerMoney);
    }



    public float getAllIncomeSourceGrossTotals(int monthAmount)
    {
        float total = 0;
        for (int i = 0; i < incomeSources().Count; i++)
        {
            total += (incomeSources()[i].getGrossIncomeAmountInAMonth() * monthAmount);
        }
        return total;
    }
    #endregion

    #region MonobehaviourDefaults
    void Start()
    {

        GameEventSystem.RegisterListener(Event_Type.FLOAT_CHANGE, SetMoney);
        GameEventSystem.RegisterListener(Event_Type.JOB_REGISTERED_TO_PLAYER, RegisterAnIncomeSourceFromJob);

        TaxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));

    }
    private void OnEnable()
    {
        DateTimeSystem.OnMonthChange += PayFromIncomeSources;
    }
    private void OnDisable()
    {
        DateTimeSystem.OnMonthChange -= PayFromIncomeSources;
    }
    #endregion


    void RegisterAnIncomeSourceFromJob(EventInfo info)
    {
        JobRegisterInfo job = (JobRegisterInfo)info;
        IncomeSource incomeSource = new IncomeSource(job.job.getMonthlyPaymentAmount());

        incomeSources().Add(incomeSource);
        TaxationSystem.calculateTaxRate(getAllIncomeSourceGrossTotals(12));
    }

    void PayFromIncomeSources()
    {
        for (int i = 0; i < incomeSources().Count; i++)
        {
            PaerToolBox.changePlayerMoney(incomeSources()[i].getNetIncomeInAMonth());
        }
    }

}
