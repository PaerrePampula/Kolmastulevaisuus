using System.Linq;

public class GameEvent
{
    #region Fields
    RandomEventScriptable scriptable; //scriptable, josta haetaan infot.
    bool isTimed;
    FIRE_LOCATION[] fire_locations; //missä sijainnissa?
    PrereqPair[] prerequisites;

    int fireTime;
    public delegate void EventSelfTrigger(GameEvent thisEvent);
    public static event EventSelfTrigger OnEventSelfTriggered;
    public delegate void EventAfterTrigger(GameEvent thisEvent);
    public static event EventAfterTrigger AfterTimedEventTriggered;
    #endregion

    #region constructor
    public GameEvent(RandomEventScriptable eventScriptable, bool isThisTimed = false, int timer = 0)
    {
        scriptable = eventScriptable;
        fire_locations = eventScriptable.fire_locations;
        prerequisites = eventScriptable.Prerequisites;

        //Timed event
        isTimed = isThisTimed;
        fireTime = timer;
        if(isTimed)
        {
            LocationHandler.OnTurnEnd += CheckForFiring;
        }
        //Timed event
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
    public void CheckForFiring()
    {
        fireTime--;

        if ((fireTime <= 0) && (fire_locations.Contains(LocationHandler.CurrentLocation.getLocation()) || fire_locations.Contains(FIRE_LOCATION.ANY)))
        {
            OnEventSelfTriggered.Invoke(this);
            LocationHandler.OnTurnEnd -= CheckForFiring;
        }
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
