using UnityEngine;
using System.Collections;

public class Bill
{
    float billAmount;
    string billName;
    int billWeeks;
    public delegate void BillingChange();
    public static event BillingChange onBillingChange;
    public delegate void BillingCreate(Bill bill);
    public static event BillingCreate onBillingCreate;
    public Bill(string instBillName, float price, int weekAmount = 0 , bool billOnce = false )
    {
        billName = instBillName;
        billAmount = price;
        billWeeks = weekAmount;

        if (billOnce == false)
        {
            ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.MonthlyListableExpenses, (billName, billAmount));

        }
        else
        {
            ListableExpense listableExpense = new ListableExpense(PlayerDataHolder.OtherListableExpenses, (billName, billAmount));
        }
        onBillingChange?.Invoke();
        onBillingCreate?.Invoke(this);
    }

}
