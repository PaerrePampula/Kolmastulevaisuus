using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Uusi vuokrattava", menuName = "VuokraKämppä")]
public class RentableHomeScriptable : ScriptableObject
{
    public string address; 
    public float baseRentAmount;
    public float waterCost;
    public float sizeInMetersSquared;

    public float electricityCost; //Simuloidaan sitten sähkönsiirto jonakin randomina arvona.
    //Tehdään myös oletus että sähkö kuuluu vuokraan.

    public float homeInsurance;
    public bool needToHaveHomeInsurance;
    public ExtrasOnRentableHomes[] extrasOnRentableHome;
    public GameObject displayPrefab;
    [TextArea]
    public string longFormDescription = "esim. uusi kolmio lähellä" +
        "keskustaa! Välittömässä läheisyydessä paljon kauppoja," +
        "sekä paikalliset korkeakoulut!";
    public string perks = "esim oma sauna, lasitettu parveke";
    public string shortFormDescription = "esim 1h + s + p";
    public string extrasDescription = "esim vuokraan sisältyy 25M laajakaista";
    public string rentableScene; //Vuokrakämpän scene
    public HuoneKoko huoneKoko;
    public VuokraTyyppi vuokraTyyppi;
    //voisi myöhemmin lisätä jonkin kuvankaappausgallerian kämpistä ehkä?
}
[System.Serializable]
public class ExtrasOnRentableHomes
{
    public string extraName;
    public float extraCostPerMonth;

}