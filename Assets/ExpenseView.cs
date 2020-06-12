using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpenseView : UiGeneric
{
    [SerializeField]
    TextMeshProUGUI monthlyExpenses;
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
        Rent rent = PlayerDataHolder.PlayerRent;
        string rentBase = rent.GetField<float>("rent").ToString();
        monthly += rentBase;
    }
}
