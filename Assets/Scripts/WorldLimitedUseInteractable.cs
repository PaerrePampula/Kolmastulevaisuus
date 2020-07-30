using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class WorldLimitedUseInteractable : WorldInteractive
{
    public delegate void InteractUse();
    public static event InteractUse onInteractUse;
    [SerializeField]
    List<FIRE_LOCATION> possibleLimitedLocations = new List<FIRE_LOCATION>();
    [SerializeField]
    Flag inCaseCantUseHereFlagFire;

    public bool CanInteract
    {
        get 
        {
            if ((PlayerDataHolder.Current.Stamina.StatFloat > 0) && checkLocationIfCanUse() == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
    public override void OnInteract()
    {
        if (CanInteract == true)
        {

            onInteractUse?.Invoke();
            base.OnInteract();
        }
        else if(checkLocationIfCanUse() == true)
        {
            Flag flag = new Flag("NO_STAMINA", 0, false);
            flag.FireFlag();
        }
        else
        {
            inCaseCantUseHereFlagFire.FireFlag();
        }


    }
    bool checkLocationIfCanUse()
    {
        if (possibleLimitedLocations.Count > 0)
        {
            if (possibleLimitedLocations.Contains( LocationHandler.Current.CurrentLocation.getLocation()))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        return true;
    }
}
