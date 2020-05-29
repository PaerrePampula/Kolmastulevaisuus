using UnityEngine;
using System.Collections;

public class Rent
{
    float rent;
    float waterCost;
    float electricityCost;
    public float getTotal()
    {
        return rent + waterCost + electricityCost;
    }
}
