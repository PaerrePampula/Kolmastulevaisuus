using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ListableExpense
{
    string expenseName;
    float expenseAmount;
    public ListableExpense(List<ListableExpense> toGoList,(string, float) expenseAndValuePair, bool stackedExpense = false)
    {
        expenseName = expenseAndValuePair.Item1;
        expenseAmount = expenseAndValuePair.Item2;
        if (stackedExpense == true)
        {
            var stackedFound = toGoList.FirstOrDefault(stacked => stacked.expenseName == expenseName);
            if (stackedFound == null)
            {
                toGoList.Add(this);
                return;
            }
            else
            {
                stackedFound.expenseAmount += expenseAndValuePair.Item2;
                return;
            }
        }
        toGoList.Add(this);
    }
    public override string ToString()
    {
        return expenseName + ": " + Mathf.Abs(expenseAmount).ToString("F2") + " €";
    }
    public float getTotal()
    {
        return Mathf.Abs(expenseAmount);
    }
}
