using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpenseView : UiGeneric
{
    [SerializeField]
    TextMeshProUGUI monthlyExpenses;
    [SerializeField]
    TextMeshProUGUI monthlyTotal;
    [SerializeField]
    TextMeshProUGUI otherExpenses;
    [SerializeField]
    TextMeshProUGUI otherTotal;

    // Start is called before the first frame update
    void Start()
    {
        populateExpense(PlayerDataHolder.Current.MonthlyListableExpenses, monthlyExpenses, monthlyTotal);
        populateExpense(PlayerDataHolder.Current.OtherListableExpenses, otherExpenses, otherTotal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void populateExpense(List<ListableExpense> expenseList, TextMeshProUGUI expenseListing, TextMeshProUGUI expenseTotaling)
    {
        string listingText = "";
        float total = 0;
        foreach (var item in expenseList)
        {
            listingText += item.ToString() + "\n";
            total += item.getTotal();
        }
        expenseListing.text = listingText;
        expenseTotaling.text = "Yhteensä: " + total.ToString() + "euroa";
    }
}
