using UnityEngine;
using System.Collections;

public class Bill
{
    float billAmount;
    string billName;
    public delegate void BillingChange();
    public static event BillingChange onBillingChange;

    public Bill(string instBillName, float price)
    {
        billName = instBillName;
        billAmount = price;
        DateTimeSystem.OnMonthChange += payBill;
        ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.Current.MonthlyListableExpenses, (billName, billAmount));
        onBillingChange.Invoke();
    }
    void payBill()
    {
        PaerToolBox.changePlayerMoney(-billAmount);
    }
}
