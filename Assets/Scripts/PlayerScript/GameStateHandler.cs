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
        maxBusts = 2;
        monthsofPlay = 4;
    }
    private void OnEnable()
    {
        DateTimeSystem.OnMonthChange += checkEnd;
        PlayerEconomy.OnBust += checkBustState;
        Flag.OnFlagFire += checkFlag;
    }
    private void OnDisable()
    {
        DateTimeSystem.OnMonthChange -= checkEnd;
        PlayerEconomy.OnBust -= checkBustState;
        Flag.OnFlagFire -= checkFlag;
    }

    static void checkEnd()
    {
        monthsofPlay--;
        if (monthsofPlay <= 0)
        {
            OnGameEnd.Invoke();
        }
    }
    static void checkFlag(Flag flag)
    {
        if (flag.FlagName == "GAME_END")
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
