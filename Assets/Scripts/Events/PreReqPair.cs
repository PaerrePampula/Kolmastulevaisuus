
using System;
using System.Collections.Generic;
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
    static T getValue<T>(PrereqPair prereq)

    {
        Type typeParameterType = typeof(T);
        if (typeParameterType == System.Type.GetType("string"))
        {
            return (T)Convert.ChangeType(prereq.StringComparatorValue, typeof(T));
        }

        else
        {
            return (T)Convert.ChangeType(prereq.FloatComparatorValue, typeof(T));
        }
    }
    public bool CheckPreRequisites() //Spagettijeesus hyökkää :/
    {

        {

            bool isApplicable = false;

                switch (TypeOfComparison)
                {

                    case ComparisonOperators.IfStatEquals:
                        if (comparisonValueType == ComparisonValueType.Float)
                        {
                            isApplicable = (isEquals<float>(this) == true) ? true : false;
                        }
                        else
                        {
                            isApplicable = (isEquals<string>(this) == true) ? true : false;
                        }
                        break;

                    case ComparisonOperators.IfStatValueIsLowerThan:
                        isApplicable = (isSmaller<float>(this) == true) ? true : false;
                        break;

                    case ComparisonOperators.IFStatValueIsHigherThan:
                        isApplicable = (isBigger<float>(this) == true) ? true : false;
                        break;

                    case ComparisonOperators.IsNotEqualTo:
                        if (comparisonValueType == ComparisonValueType.Float)
                        {
                            isApplicable = (isEquals<float>(this) == false) ? true : false;
                        }
                        else
                        {
                            isApplicable = (isEquals<string>(this) == false) ? true : false;
                        }
                        break;

                    case ComparisonOperators.IfStatDoesntExist:
                        IStattable stat = StatsChecker.getPlayerStatByPrereq(this);
                        return (stat == null) ? true : false;

                    case ComparisonOperators.IfStatIsAtleast:
                        isApplicable = ((isBigger<float>(this) == true) | (isEquals<float>(this) == true)) ? true : false;
                        break;
                    case ComparisonOperators.IfStatIsAtMost:
                        isApplicable = ((isSmaller<float>(this) == true) | (isEquals<float>(this) == true)) ? true : false;
                        break;



            }
            return isApplicable;
        }
    }
    bool isBigger <T>(PrereqPair prereq)
    {
        IStattable stat = StatsChecker.getPlayerStatByPrereq(prereq);
        bool isApplicable = false;
        T comparisonValue = stat.getValue<T>();
        T eventValue = getValue<T>(prereq);

        isApplicable = (Comparer<T>.Default.Compare(comparisonValue, eventValue) > 0) ? true : false;
        return isApplicable;
    }
    bool isSmaller <T>(PrereqPair prereq)
    {
        IStattable stat = StatsChecker.getPlayerStatByPrereq(prereq);
        bool isApplicable = false;
        T comparisonValue = stat.getValue<T>();
        T eventValue = getValue<T>(prereq);

        isApplicable = (Comparer<T>.Default.Compare(comparisonValue, eventValue) < 0) ? true : false;
        return isApplicable;
    }
    bool isEquals <T>(PrereqPair prereq)
    {
        IStattable stat = StatsChecker.getPlayerStatByPrereq(prereq);
        bool isApplicable = false;
        T comparisonValue = stat.getValue<T>();
        T eventValue = getValue<T>(prereq);
        isApplicable = (Comparer<T>.Default.Compare(comparisonValue, eventValue) == 0) ? true : false;

        return isApplicable;
    }

}
