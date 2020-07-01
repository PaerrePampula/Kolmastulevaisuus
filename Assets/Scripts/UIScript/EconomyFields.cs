using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EconomyFields : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI currentEconomyText;
    [SerializeField]
    TextMeshProUGUI incomeEconomyText;
    [SerializeField]
    TextMeshProUGUI expenseEconomyText;
    [SerializeField]
    GameObject incText;
    float currentFloat;
    float incomeFloat;
    float expenseFloat;
    private delegate float gottenValueMethod();

    #region MonobehaviourDefaults
    private void OnEnable() //Kun tämä skripti aktivoituu, se automaattisesti tilaa Kukkaroskriptin OnIncrease tapahtuman
    {
        PlayerEconomy.OnMoneyChange += UpdateUI;
        PlayerEconomy.OnNewIncome += UpdateUI;
        Bill.onBillingChange += UpdateUI;
        UpdateUI();

    }
    private void OnDisable()
    {
        PlayerEconomy.OnMoneyChange -= UpdateUI; //Jos tämä skripti poistuu, se ottaa sen tilauksen ensin pois. Miksi? Koska muuten tulisi null reference exceptioneita, jos tilaus on olemassa, mutta ei vastaanottajaa...
        PlayerEconomy.OnNewIncome += UpdateUI;
        Bill.onBillingChange -= UpdateUI;
        StopAllCoroutines();
    }
    #endregion
    void UpdateUI(float amount) //Tämä on se metodi, joka lähtee automaattisesti raksuttamaan, jos skripti saa tietää kukkarossa tapahtuneesta muutoksesta. Tehokkaampaa kuin samankaltaisen metodin länttääminen updateen joka kutsuisi tätä joka ikinen frame....
    {


        StartCoroutine(startIncrementing(currentEconomyText));

    }
    void UpdateUI()
    {
        incomeEconomyText.text = (PlayerEconomy.totalNetIncomeInAMonth() + " euroa/kk");
        expenseEconomyText.text = (PlayerDataHolder.Current.getTotalCosts()) + " euroa/kk";
    }
    public IEnumerator startIncrementing(TextMeshProUGUI text)
    {

        float t = 0.0f;
        float originalvalue = currentFloat;
        gottenValueMethod method = PlayerDataHolder.Current.PlayerMoney.getValue<float>;

        while (currentFloat != method())
        {
            currentFloat = Mathf.Lerp(originalvalue, method(), t);
            t += 0.75f * Time.deltaTime;
            text.text = System.Math.Round(currentFloat, 2).ToString() + " euroa";
            yield return null;

        }

    }
    private void Start()
    {
        UpdateUI();
    }
}
