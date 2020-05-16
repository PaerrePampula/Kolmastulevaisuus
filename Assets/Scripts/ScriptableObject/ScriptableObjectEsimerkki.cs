using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Esimerkki scriptableobject", menuName = "Esimerkki scriptableobjectista")]
public class ScriptableObjectEsimerkki : ScriptableObject //Scriptableobject ei ole osa monobehaviouria (ei voi muun muuassa kutsua suoraltaan esim GameObjecteja, mutta sen toimintaperiaatteeltaan ihan hyvä asia.
{
    //Mikä tämä on? Tämä on esimerkki siitä mikä on scriptableobject, joka on unitylle ainutlaatuinen skriptityyppi
    //Mitä se tekee?
    //Scriptableobject on tietynlainen data-containeri, ei varsinaisesti joku skripti, jonka voi kiinnittää johonkin pelinsisäiseen peliobjektiin, mutta sitä voi käyttää
    //sisällyttämään tietoa. Esimerkkinä dialogiteksti. Jossakin pelissä voisi olla esimerkiksi dialogiboksi, joka hakee tekstinsä jostain tietystä scriptableobjectista.
    //scriptableobjectiin tehdyt muutokset ovat play-moden sulkemisen jälkeen pysyviä, ja sitä pystyy muokata runtimessa, esim esimerkkidialogin tekstiä voisi muuttaa kesken pelin, ja uutta
    //dialogia pyytäessä dialogi muuttuisi.
    //muita esimerkkejä mahdollisista scriptableobjecteista voisi olla ennalta valittu inventorysisältö, itemin statsit, tai laatikon sisältö jne. vaikka jossakin rpg:sä tai vaikka "vihollistyyppi".
    //Esimerkissä on, no, esimerkki tämän ominaisuuden käytössä.
    //scriptableobjecteilla on myös huom vaara siihen, että data voi tuhoutua, jos sitä käyttää väärin, niin sen muuttamista kannattaa välttää, silloin kun ei halua että alkuperäinen
    //Scriptableobject muuttuu. (Muutokset runtimessä käsin tai skriptin kautta scriptableobjectissa on pysyviä, niin niitä kannattaisi pääasiassa käsitellä viittauksina toisissa classeissä.)
    //Miksi tämä on täällä?
    //Sullon tähän peliin muutaman scriptableobjectin muun muuassa random eventtien avuksi, joten ajattelin että tämä tiedosto olisi ihan hyödyksi, jos haluaa tietää mitä skripteissä tapahtuu.



    //Ajatellaan että halutaan jonkin generic rpg:n dialogi tähän, tehdään tästä esimerkki.
    public string[] dialogtext; //Määritellään array stringejä, jota voi inspectorista käsin muokata, sen takia niiden tarvitsee olla myös publiceja.
    public Sprite dialogCharacterImage; //Tässä on geneeriseen rpg dialogi tyyliin myös sopiva hahmokuva...
    public advancedDialog[] advancedDialogs; //...Mutta tällä voidaan tehdä paljon enemmänkin! Todetaan array advancedDialogeja, ja määritellään se alla!
}
[System.Serializable] //Tarvitaan tämä, tai joudutaan tyytymään erikseen luotuihin dialogeihin = ei pystyisi suoraan samasta inspectorista luomaan.
public class advancedDialog 
{
    public string dialogText; //Taas tekstejä...
    public Sprite reactionImage; //...ja erikseen jokaiselle tekstille hahmon reaktio!
}
