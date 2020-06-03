
using System;
using UnityEngine;

[Serializable]
public class PrereqPair
{
    public StatType playerStat;
    public ComparisonValueType comparisonValueType;
    public string StringComparatorValue;
    public float FloatComparatorValue;
    public bool uniqueStatComparison;
    public ComparisonOperators TypeOfComparison;
}