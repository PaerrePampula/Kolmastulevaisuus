public class GameEvent
{
    #region Fields
    RandomEventScriptable scriptable; //scriptable, josta haetaan infot.
    bool isUniqueEvent; //Tapahtuu vain kerran ikinä?
    bool hasFired; //Onko tämä event firenny ennenkin?
    FIRE_LOCATION[] fire_locations; //missä sijainnissa?
    PrereqPair[] prerequisites;
    float requirementTargetFloat;
    float playerStatFloat;
    #endregion

    #region constructor
    public GameEvent(RandomEventScriptable eventScriptable)
    {
        scriptable = eventScriptable;
        fire_locations = eventScriptable.fire_locations;
        prerequisites = eventScriptable.Prerequisites;
    }
    #endregion

    #region getters
    public RandomEventScriptable getData() //Hakee siis scriptablen eventistä tiedonhallintaa varten.
    {
        return scriptable;
    }
    public FIRE_LOCATION[] getFireLocations()
    {
        return fire_locations;
    }
    #endregion

    public bool CheckPreRequisites()
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
