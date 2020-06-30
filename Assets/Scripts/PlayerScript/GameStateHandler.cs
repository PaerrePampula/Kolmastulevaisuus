using UnityEngine;
using System.Collections;

public class GameStateHandler : MonoBehaviour
{
    public delegate void Damage(int strikeCost);
    public static event Damage OnDamage;
    public delegate void GameEnd();
    public static event GameEnd OnGameEnd;
    static int maxBusts = 2;
    static int monthsofPlay = 4;
    public void Start()
    {
        DateTimeSystem.OnMonthChange += checkEnd;
        maxBusts = 2;
        PlayerEconomy.OnBust += checkBustState;
    }


    static void checkEnd()
    {
        monthsofPlay--;
        if (monthsofPlay <= 0)
        {
            OnGameEnd.Invoke();
        }
    }
    static void checkBustState(int strikes)
    {
        maxBusts -= strikes;
        OnDamage.Invoke(strikes);

        if (maxBusts < 0)
        {
            gameFailEnd();
        }
        if (maxBusts > 0)
        {
            Flag flag = new Flag("PLAYER_LOST_HP",0,false);
            flag.FireFlag();
            PlayerDataHolder.Current.PlayerMoney.resetMoney();
            PaerToolBox.changePlayerMoney(100);
            
        }
    }
    static void gameFailEnd()
    {
        Flag flag = new Flag("PLAYER_FAIL", 0, true);
        flag.FireFlag();
    }
}
