using UnityEngine;
using System.Collections;

public class PlayerMoney : IStattable
{
    
    float amount;
    bool usableMoney;
    public bool UniqueStat { get { return true; } }
    public StatType ThisStatType { get { return StatType.PlayerMoney; } }
    public delegate void NewMoneyChange(float moneyAmount);
    public event NewMoneyChange onMoneyChange;
    public delegate void MoneyAlert(int strikeCost);
    public  event MoneyAlert OnBust;
    public PlayerMoney(bool usable)
    {
        if (usable)
        {
            usableMoney = true;
            PlayerEconomy.onExpensesPay += checkForBelowZero;
            ResetButton.onReset += unInit;
            StatsChecker.RegisterStat(this);
        }

    }
    void checkForBelowZero()
    {
        if (amount < 0)
        {
            float difference = amount;
            resetMoney();
            if (Bank.Current.SavedMoney.getValue<float>() + difference >= 0)
            {
                Bank.Current.SavedMoney.MoneyChange(difference);

                Flag savingsSave = new Flag("PLAYER_ECONOMY_SAVED_BY_SAVINGS", 0, false);
                savingsSave.FireFlag();

                return;
            }
            else
            {
                Bank.Current.SavedMoney.resetMoney();
                OnBust?.Invoke(1);
            }

        }
    }
    void unInit()
    {
        PlayerEconomy.onExpensesPay -= checkForBelowZero;
        ResetButton.onReset -= unInit;
    }
    public T getValue<T>()
    {

        return (T)(object)amount;
    }
    public void MoneyChange(float Chgamount)
    {
        Debug.Log(amount);
        amount += Chgamount;

        onMoneyChange?.Invoke(amount);
    }
    public void resetMoney()
    {
        amount = 0;
        onMoneyChange?.Invoke(amount);
    }
}
