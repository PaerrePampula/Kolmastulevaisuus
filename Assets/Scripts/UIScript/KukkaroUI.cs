using TMPro;
using UnityEngine;

public class KukkaroUI : MonoBehaviour
{


    #region MonobehaviourDefaults
    private void OnEnable() //Kun tämä skripti aktivoituu, se automaattisesti tilaa Kukkaroskriptin OnIncrease tapahtuman
    {
        PlayerEconomy.OnIncrease += UpdateUI;
    }
    private void OnDisable()
    {
        PlayerEconomy.OnIncrease -= UpdateUI; //Jos tämä skripti poistuu, se ottaa sen tilauksen ensin pois. Miksi? Koska muuten tulisi null reference exceptioneita, jos tilaus on olemassa, mutta ei vastaanottajaa...
    }
    #endregion
    void UpdateUI(float amount) //Tämä on se metodi, joka lähtee automaattisesti raksuttamaan, jos skripti saa tietää kukkarossa tapahtuneesta muutoksesta. Tehokkaampaa kuin samankaltaisen metodin länttääminen updateen joka kutsuisi tätä joka ikinen frame....
    {
        transform.GetComponent<TextMeshProUGUI>().text = "Pelaajalla on " + amount + " euroa!";
    }
}
