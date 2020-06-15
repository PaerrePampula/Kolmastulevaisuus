﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListableExpense
{
    string expenseName;
    float expenseAmount;
    public ListableExpense(List<ListableExpense> toGoList,(string, float) expenseAndValuePair)
    {
        expenseName = expenseAndValuePair.Item1;
        expenseAmount = expenseAndValuePair.Item2;
        toGoList.Add(this);
    }
    public override string ToString()
    {
        return expenseName + ": " + expenseAmount;
    }
    public float getTotal()
    {
        return expenseAmount;
    }
}