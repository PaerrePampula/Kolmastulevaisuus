using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Uusi työpaikkailmoitus", menuName = "Työpaikkailmoitus")]
public class JobNoticeScriptable : ScriptableObject
{
    [Header("Nimi ja kuvaus")]
    public string jobTitle;
    [TextArea]
    public string jobDescriptionOnNotice;

    [Header("Palkka ja tunnit")]
    public int minimumHoursPerWeek;
    public int maximumHoursPerWeek;
    public float payByHour;
    [Header("Aika")]
    public int jobLengthInMonths;
    [Header("Mahdollisuuskertoimet")]
    [Tooltip("Kun pelaaja hakee monta työpaikkaa samaan aikaan, tämä kuvaa kerrointa siitä,\n"
        + "Että kuinka todennäköistä on että juuri tämä työpaikka valitaan. Isompi kerroin on isompi mahdollisuus" +
        "\n\nEi ole prosentti, tämä suhteutetaan vain kertoimena työpaikkahakujen määrään")]
    public int successMultiplier;
    [Tooltip("Kuvastaa välillä 0-100% että kuinka todennäköistä on että tämä työhaku lisätään työpaikan arvontaan")]
    public int chanceOfBeingHired;

    //Boolit eri vaatimuksille? Esim jotain kortteja, hygienipassi? ajokortti? jotain muuta?

    [Header("Scriptablet")]
    public string jobSiteScene;
    //Pitäisi varmaan tehdä joku osa-aika, kokoaika, provikkapalkka, muu tyyyppinen palkka simulaatio vielä...
    [Tooltip("Työpaikan mahdolliset eventit")]
    public List<RandomEventScriptable> jobEvents;
    
}
