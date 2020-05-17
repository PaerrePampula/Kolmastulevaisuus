using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KukkaroUI : MonoBehaviour
{
    Kukkaro kukkaro; //Tämä on myös prototyyppi, ja tullaan korvaamaan jollakin järkevämmällä todennäköisesti, mutta tänhetkisesti sen pointti on demota rahanmuutosta suoraan play-ikkunaan
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable() //Kun tämä skripti aktivoituu, se automaattisesti tilaa Kukkaroskriptin OnIncrease tapahtuman
    {
        Kukkaro.OnIncrease += UpdateUI;
    }
    private void OnDisable()
    {
        Kukkaro.OnIncrease -= UpdateUI; //Jos tämä skripti poistuu, se ottaa sen tilauksen ensin pois. Miksi? Koska muuten tulisi null reference exceptioneita, jos tilaus on olemassa, mutta ei vastaanottajaa...
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI(float amount) //Tämä on se metodi, joka lähtee automaattisesti raksuttamaan, jos skripti saa tietää kukkarossa tapahtuneesta muutoksesta. Tehokkaampaa kuin samankaltaisen metodin länttääminen updateen joka kutsuisi tätä joka ikinen frame....
    {
        transform.GetComponent<TextMeshProUGUI>().text = "Pelaajalla on " + amount + " euroa!";
    }
}
