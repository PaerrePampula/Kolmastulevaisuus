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

    // Start is called before the first frame update
    void Start()
    {
        populateMonthly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void populateMonthly()
    {
        string monthly = "";
        float total = 0;
        foreach (var item in PlayerDataHolder.MonthlyListableExpenses)
        {
            monthly += item.ToString() + "\n";
            total += item.getTotal();
        }
        monthlyExpenses.text = monthly;
        monthlyTotal.text = "Yhteensä: " + total.ToString() + "euroa";
    }
}
