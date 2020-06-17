
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

    public static bool CheckPreRequisites(PrereqPair[] prerequisites)
    {
        if (prerequisites.Length < 1)
        {
            return true;
        }
        else
        {
            bool isApplicable = false;
            foreach (var prerequisite in prerequisites)
            {
                IStattable stat = StatsChecker.getPlayerStatByPrereq(prerequisite);
                isApplicable = ((stat == null) && prerequisite.TypeOfComparison == ComparisonOperators.IfStatDoesntExist) ? true : false;
                switch (prerequisite.comparisonValueType)
                {
                    case ComparisonValueType.Float:
                        float comparisonValue = stat.getValue<float>();
                        float eventValue = prerequisite.FloatComparatorValue;
                        isApplicable = ((comparisonValue > eventValue) && (prerequisite.TypeOfComparison == ComparisonOperators.IFStatValueIsHigherThan
                            ))
                            || ((comparisonValue < eventValue) && (prerequisite.TypeOfComparison == ComparisonOperators.IfStatValueIsLowerThan))
                            || ((comparisonValue == eventValue) && (prerequisite.TypeOfComparison == ComparisonOperators.IfStatEquals)) ? true : false;
                        break;

                    case ComparisonValueType.String:
                        string comparisonString = stat.getValue<string>();
                        string eventString = prerequisite.StringComparatorValue;
                        isApplicable = ((comparisonString == eventString) && (prerequisite.TypeOfComparison == ComparisonOperators.IfStatEquals)) ||
                            ((comparisonString != eventString) && (prerequisite.TypeOfComparison == ComparisonOperators.IsNotEqualTo)) ? true : false;
                        break;
                    default:
                        break;
                }
                if (isApplicable == false) return false;
            }
            return isApplicable;
        }
    }
}
