﻿public class GameEvent
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
        if (prerequisites.Length == 0)
        {
            return true;

        }
        else
        {
            bool isApplicableEvent = false;
            foreach (PrereqPair prerequisite in prerequisites) //Jotenkin vois tämän kamalan roinan korvata? Turhia parse erroreita sun muita outoja pakotteita täällä arvojen muodoille, iso switch case helvetti jne.
            {
                PlayerStat foundPair = PlayerStatContainer.Current.getPlayerStatByPrereq(prerequisite);
                if (prerequisite.TypeOfComparison != ComparisonOperators.IfStringValueEqualsStatString)
                {
                    if (foundPair != null)
                    {
                        requirementTargetFloat = float.Parse(prerequisite.StringComparatorValue);
                        playerStatFloat = foundPair.statValueFloat;
                    }

                }



                switch (prerequisite.TypeOfComparison)
                {

                    case ComparisonOperators.IfPlayerHasHigher:
                        isApplicableEvent = (requirementTargetFloat < playerStatFloat) ? true : false;
                        break;
                    case ComparisonOperators.IfPlayerHasLower:
                        isApplicableEvent = (requirementTargetFloat > playerStatFloat) ? true : false;
                        break;
                    case ComparisonOperators.IfPlayerStatEquals:
                        isApplicableEvent = (requirementTargetFloat == playerStatFloat) ? true : false;
                        break;
                    case ComparisonOperators.IfPlayerValueIsAtleast:
                        isApplicableEvent = (requirementTargetFloat <= playerStatFloat) ? true : false;
                        break;
                    case ComparisonOperators.IfStringValueEqualsStatString:
                        isApplicableEvent = (prerequisite.StringComparatorValue == foundPair.statValueString) ? true : false;
                        break;
                    case ComparisonOperators.IfPlayerHasStat:
                        isApplicableEvent = ((foundPair == null && prerequisite.StringComparatorValue == "false") || foundPair != null && prerequisite.StringComparatorValue == "true") ? true : false;
                        break;
                }

            }
            return isApplicableEvent;
        }

    }
}
