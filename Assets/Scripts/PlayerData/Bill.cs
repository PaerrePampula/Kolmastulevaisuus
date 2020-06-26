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
    public Bill(string instBillName, float price)
    {
        billName = instBillName;
        billAmount = price;
        DateTimeSystem.OnMonthChange += payBill;
        ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.MonthlyListableExpenses, (billName, billAmount));
        onBillingChange?.Invoke();
        onBillingCreate?.Invoke(this);
    }
    void payBill()
    {
        PaerToolBox.changePlayerMoney(-billAmount);
    }
}
