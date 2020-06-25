using UnityEngine;
using System.Collections;

public class WorldLimitedUseInteractable : WorldInteractive
{
    public delegate void InteractUse();
    public static event InteractUse onInteractUse;

    public bool CanInteract
    {
        get 
        {
            if (PlayerDataHolder.Current.LimitedUseWorldInteractableStamina > 0)
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
        else
        {
            Flag flag = new Flag("NO_STAMINA", 0, false);
            flag.FireFlag();
        }


    }
}
