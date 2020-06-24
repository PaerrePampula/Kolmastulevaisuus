using UnityEngine;
using System.Collections;

public class Bank : MonoBehaviour
{
    PlayerMoney savedMoney;

    public PlayerMoney SavedMoney { get => savedMoney; set => savedMoney = value; }

    // Use this for initialization
    void OnEnable()
    {
        Flag.OnFlagFire += checkFlag;
        SavedMoney = new PlayerMoney();
    }
    private void OnDestroy()
    {
        Flag.OnFlagFire -= checkFlag;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void checkFlag(Flag flag)
    {
        switch (flag.FlagName)
        {
            case "SAVING_50":
                addSavings(50);
                break;
            case "SAVING_25":
                addSavings(25);
                break;
            default:
                break;
        }
    }
    void addSavings(float amount)
    {
        SavedMoney.MoneyChange(amount);
    }
}
