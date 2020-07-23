using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpenseView : UiGeneric
{

    [SerializeField]
    TextMeshProUGUI monthlyTotal;
    [SerializeField]
    TextMeshProUGUI otherTotal;
    [SerializeField]
    GameObject prefabbedTextObject;
    [SerializeField]
    Transform monthlyGrid;
    [SerializeField]
    Transform otherExpensesGrid;
    public delegate void CompletedTask();
    public static event CompletedTask onEventComplete;
    // Start is called before the first frame update
    void Start()
    {

        createTextsForGrid(PlayerDataHolder.OtherListableExpenses, otherTotal, otherExpensesGrid);
        createTextsForGrid(PlayerDataHolder.MonthlyListableExpenses, monthlyTotal, monthlyGrid);
    }

    void createTextsForGrid(List<ListableExpense> expenseList, TextMeshProUGUI expenseTotaling, Transform grid)
    {
        float total = 0;
        for (int i = 0; i < expenseList.Count; i++)
        {
            GameObject nameObject = Instantiate(prefabbedTextObject);
            nameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = expenseList[i].getName();
            GameObject costObject = Instantiate(prefabbedTextObject);
            costObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = expenseList[i].getTotal().ToString() + " euroa/kk";
            nameObject.transform.SetParent(grid);
            costObject.transform.SetParent(grid);
            total += expenseList[i].getTotal();
        }
        onEventComplete?.Invoke();
        expenseTotaling.text = "Yhteensä: " + total.ToString() + " euroa/kk";
    }
}
