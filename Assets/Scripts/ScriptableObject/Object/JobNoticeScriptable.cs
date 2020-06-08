using UnityEngine;
[CreateAssetMenu(fileName = "Uusi työpaikkailmoitus", menuName = "Työpaikkailmoitus")]
public class JobNoticeScriptable : ScriptableObject
{
    public string jobTitle;
    [TextArea]
    public string jobDescriptionOnNotice;
    [Tooltip("Kuvaa siis kerrointa, joka otetaan huomioon siinä, kun monta hakemusta on lähetetty" +
        "isompi kerroin kuvastaa isompaa todennäköisyyttä \n" +
        "Laskukaava on (normalizer * multiplier), normalizer on 1/(n1+n2+n3...)\n" +
        "Asettaa siis luvut riippuen lukujen lukumäärästä välille 0-1\n"
        +"Esimerkiksi \n0-20 Melko matala chance\n 20-60 keskimääräinen chance 60-100 korkea chance \n" +
        "Luku voi olla myös isompi kuin 100, mutta sen chanssi on vain tosi korkea silloin")]
    public int successMultiplier;
    //Boolit eri vaatimuksille? Esim jotain kortteja, hygienipassi? ajokortti? jotain muuta?
    public int workHoursPerDay;
    public float payByHour;
    public JobSiteScriptable jobSite;
    //Pitäisi varmaan tehdä joku osa-aika, kokoaika, provikkapalkka, muu tyyyppinen palkka simulaatio vielä...
}
