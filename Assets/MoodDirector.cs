using UnityEngine;
using System.Collections;

public class MoodDirector : MonoBehaviour
{

    private void OnEnable()
    {
        LocationHandler.OnTurnEnd += changeMood; //Joka kerta kun kierros on suoritettu, vähennä pelaajan hyvinvointia, jotta sitä tarvitsisisi huoltaa
    }
    void changeMood()
    {
        float changeAmount = ((100 - PlayerDataHolder.Current.Comfortableness.StatFloat)/100 + (0.25f)) * 12;
        PlayerDataHolder.Current.Satisfaction.ChangeStat(-changeAmount);
    }
}
