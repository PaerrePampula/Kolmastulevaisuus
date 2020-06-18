using UnityEngine;
using System.Collections;

public class HungerSystem : MonoBehaviour
{

    private void OnEnable()
    {
        LocationHandler.OnLocationChange += incrementHunger;

    }
    void incrementHunger()
    {
        PlayerDataHolder.Current.Hunger.ChangeStat(7.5f);
    }
}
