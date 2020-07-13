using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI usableFundsIndicator;
    [SerializeField]
    TextMeshProUGUI savedFundsIndicator;
    // Start is called before the first frame update
    private void OnEnable()
    {
        usableFundsIndicator.text = PlayerDataHolder.Current.PlayerMoney.getValue<float>().ToString();
        savedFundsIndicator.text = Bank.Current.ToString();
        PlayerDataHolder.Current.PlayerMoney.onMoneyChange += UpdateUI;
        Bank.Current.SavedMoney.onMoneyChange += UpdateSavings;
    }
    private void OnDisable()
    {
        PlayerDataHolder.Current.PlayerMoney.onMoneyChange -= UpdateUI;
        Bank.Current.SavedMoney.onMoneyChange -= UpdateSavings;
    }
    void UpdateUI(float amount)
    {
        usableFundsIndicator.text = amount.ToString();
    }
    void UpdateSavings(float amount)
    {
        savedFundsIndicator.text = amount.ToString();
    }

}
