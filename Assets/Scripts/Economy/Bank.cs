using UnityEngine;
using System.Collections;

public class Bank : MonoBehaviour
{
    PlayerMoney savedMoney = new PlayerMoney();
    public delegate void MoneySave();
    public static event MoneySave onMoneySave;
    public PlayerMoney SavedMoney
    {
        get => savedMoney; set
        {
            savedMoney = value;

        }
    }
    static private Bank _Current;
    static public Bank Current
    {
        get
        {
            if (_Current == null)
            {
                _Current = FindObjectOfType<Bank>();
            }
            return _Current;
        }
    }
    public override string ToString()
    {
        return savedMoney.getValue<float>().ToString();
    }

    // Use this for initialization
    void OnEnable()
    {
        Flag.OnFlagFire += checkFlag;

    }
    private void OnDestroy()
    {
        Flag.OnFlagFire -= checkFlag;
    }

    void checkFlag(Flag flag)
    {
        if (flag.FlagName == "SAVING")
        {
            addSavings(flag.flagOptionalValue);
        }

    }
    void addSavings(float amount)
    {
        SavedMoney.MoneyChange(amount);
        onMoneySave.Invoke();
    }
    public void DEBUG_ADDSAVINGS(float amount)
    {
        SavedMoney.MoneyChange(amount);
    }

}
