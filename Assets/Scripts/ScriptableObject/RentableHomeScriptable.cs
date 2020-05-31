using UnityEngine;
using System.Collections;

public class RentableHomeScriptable : MonoBehaviour
{
    public string address; 
    public float baseRentAmount;
    public float waterCost;

    public float electricityCost; //Simuloidaan sitten sähkönsiirto jonakin randomina arvona.
    //Tehdään myös oletus että sähkö kuuluu vuokraan.

    public float homeInsurance;
    public bool needToHaveHomeInsurance;
    public (string, float)[] extrasCost; //esim saunan hinta, autoparkin hinta.

    public string longFormDescription = "esim. uusi kolmio lähellä" +
        "keskustaa! Välittömässä läheisyydessä paljon kauppoja," +
        "sekä paikalliset korkeakoulut!";
    public string perks = "esim oma sauna, lasitettu parveke";
    public string shortFormDescription = "esim 1h + s + p";
    public string extrasDescription = "esim vuokraan sisältyy 25M laajakaista";
    public GameObject prefab; //Vuokrakämpän malli.
    //voisi myöhemmin lisätä jonkin kuvankaappausgallerian kämpistä ehkä?
}
