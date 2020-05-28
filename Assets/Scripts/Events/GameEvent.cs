using UnityEngine;
using System.Collections;
using System;

public class GameEvent
{
    RandomEventScriptable scriptable;
    bool isUniqueEvent; //Tapahtuu vain kerran ikinä?
    bool hasFired; //Onko tämä event firenny ennenkin?
    FIRE_LOCATION fire_location;
    PrereqPair[] prerequisites;

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
            bool boolean = false;
            foreach (PrereqPair pair in prerequisites)
            {
                float thisEventFloat = float.Parse(pair.stringValue);
                PrereqPair foundPair = PlayerStatContainer.Current.getPairByPreReq(pair);
                float comparedStatFloat = float.Parse(foundPair.stringValue);
                switch (pair.typeofPreReq)
                {
                    
                    case PreReqTypes.valueComparePlayerHasLower:
                        if (thisEventFloat > comparedStatFloat)
                        {
                            boolean = true;
                            break;

                        }
                        else
                        {

                            boolean = false;
                            break;
                        }

                    case PreReqTypes.ValueComparePlayerHasHigher:
                        if (thisEventFloat < comparedStatFloat)
                        {
                            boolean = true;
                            break;

                        }
                        else
                        {
                            boolean = false;
                            break;
                        }

                    case PreReqTypes.Boolean:

                        break;
                    case PreReqTypes.StringCompare:
                        if (pair.stringValue == foundPair.stringValue)
                        {
                            boolean = true;
                            break;
                        }
                        else
                        {
                            boolean = false;
                            break;
                        }

                    case PreReqTypes.PlayerHasvalueEquals:
                        if (thisEventFloat == comparedStatFloat)
                        {
                            boolean = true;
                            break;

                        }
                        else
                        {
                            boolean = false;
                            break;
                        }
                    default:
                        break;
                }

            }
            return boolean;
        }

    }
}
