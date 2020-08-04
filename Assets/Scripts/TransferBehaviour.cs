using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransferBehaviour : MonoBehaviour
{
    PlayerMoney sourceMoney;
    PlayerMoney destinationMoney;
    [SerializeField]
    TextMeshProUGUI sourceAmount;
    [SerializeField]
    TextMeshProUGUI destinationAmount;
    [SerializeField]
    TMP_InputField inputField;
    public void Initialize(PlayerMoney money1, PlayerMoney money2)
    {
        sourceMoney = money1;
        destinationMoney = money2;
        sourceAmount.text = sourceMoney.getValue<float>().ToString();
        destinationAmount.text = destinationMoney.getValue<float>().ToString();
    }
    public void checkMax()
    {
        inputField.text = Mathf.Clamp(float.Parse(inputField.text) , 0, sourceMoney.getValue<float>()).ToString();
    }
    public void TransferFunds()
    {
        sourceMoney.MoneyChange(-float.Parse(inputField.text));
        destinationMoney.MoneyChange(float.Parse(inputField.text));
    }

}
