﻿using UnityEngine;
using System.Collections;
using System;

public class GameEvent
{
    RandomEventScriptable scriptable;
    bool isUniqueEvent; //Tapahtuu vain kerran ikinä?
    bool hasFired; //Onko tämä event firenny ennenkin?
    FIRE_LOCATION fire_location;
    PrereqPair[] prerequisites;
    float requirementTargetFloat;
    float playerStatFloat;

    public GameEvent(RandomEventScriptable eventScriptable)
    {
        scriptable = eventScriptable;
        fire_location = eventScriptable.fire_location;
        prerequisites = eventScriptable.Prerequisites;
    }
    public RandomEventScriptable getData() //Hakee siis scriptablen eventistä tiedonhallintaa varten.
    {
        return scriptable;
    }
    public FIRE_LOCATION getFireLocation()
    {
        return fire_location;
    }
    public bool CheckPreRequisites()
    {
        if (prerequisites.Length == 0)
        {
            return true;

        }
        else
        {
            bool isApplicableEvent = false;
            foreach (PrereqPair prerequisite in prerequisites)
            {
                PlayerStat foundPair = PlayerStatContainer.Current.getPlayerStatByPrereq(prerequisite);
                if (prerequisite.TypeOfComparison != ComparisonOperators.IfStringValueEqualsStatString)
                {
                    if (foundPair != null)
                    {
                        requirementTargetFloat = float.Parse(prerequisite.StringComparatorValue);
                        playerStatFloat = foundPair.statValueToFloat();
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
                        isApplicableEvent = ((foundPair == null && prerequisite.StringComparatorValue == "false") || foundPair != null && prerequisite.StringComparatorValue == "true" ) ? true : false;
                        break;
                }

            }
            return isApplicableEvent;
        }

    }
}
