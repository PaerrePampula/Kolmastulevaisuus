using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PrereqPair
{
    public StatType playerStat;
    public string StringComparatorValue;
    public bool uniqueStatComparison;
    public ComparisonOperators TypeOfComparison;
}
