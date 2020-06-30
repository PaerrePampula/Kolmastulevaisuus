using UnityEngine;
using System.Collections;

public class Bill
{
    float billAmount;
    string billName;
    public delegate void BillingChange();
    public static event BillingChange onBillingChange;
    public delegate void BillingCreate(Bill bill);
    public static event BillingCreate onBillingCreate;
    public Bill(string instBillName, float price, bool billOnce = false )
    {
        billName = instBillName;
        billAmount = price;

        if (billOnce == false)
        {
            ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.MonthlyListableExpenses, (billName, billAmount));
            DateTimeSystem.OnMonthChange += payBill;
        }
        else
        {
            ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.OtherListableExpenses, (billName, billAmount));
        }
        onBillingChange?.Invoke();
        onBillingCreate?.Invoke(this);
    }
    void payBill()
    {
        PaerToolBox.changePlayerMoney(-billAmount);
    }
}
