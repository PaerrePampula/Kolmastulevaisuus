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
    float currentFloat;
    float incomeFloat;
    float expenseFloat;
    private delegate float gottenValueMethod();

    #region MonobehaviourDefaults
    private void OnEnable() //Kun tämä skripti aktivoituu, se automaattisesti tilaa Kukkaroskriptin OnIncrease tapahtuman
    {
        PlayerEconomy.OnIncrease += UpdateUI;
        PlayerEconomy.OnNewIncome += UpdateUI;

        UpdateUI();

    }
    private void OnDisable()
    {
        PlayerEconomy.OnIncrease -= UpdateUI; //Jos tämä skripti poistuu, se ottaa sen tilauksen ensin pois. Miksi? Koska muuten tulisi null reference exceptioneita, jos tilaus on olemassa, mutta ei vastaanottajaa...
        PlayerEconomy.OnNewIncome += UpdateUI;
    }
    #endregion
    void UpdateUI(float amount) //Tämä on se metodi, joka lähtee automaattisesti raksuttamaan, jos skripti saa tietää kukkarossa tapahtuneesta muutoksesta. Tehokkaampaa kuin samankaltaisen metodin länttääminen updateen joka kutsuisi tätä joka ikinen frame....
    {
        StartCoroutine(startIncrementing(currentEconomyText));

    }
    void UpdateUI()
    {
        incomeEconomyText.text = PlayerEconomy.totalNetIncomeInAMonth() + " euroa";
        expenseEconomyText.text = PlayerDataHolder.PlayerRent.getValue<float>() + " euroa";
    }
    public IEnumerator startIncrementing(TextMeshProUGUI text)
    {

        float t = 0.0f;
        float originalvalue = currentFloat;
        gottenValueMethod method = PlayerDataHolder.PlayerMoney.getValue<float>;

        while (currentFloat != method())
        {
            currentFloat = Mathf.Lerp(originalvalue, method(), t);
            t += 0.75f * Time.deltaTime;
            text.text = System.Math.Round(currentFloat, 2).ToString() + " euroa";
            yield return null;

        }

    }
}
