using UnityEngine;
[CreateAssetMenu(fileName = "Uusi työpaikkailmoitus", menuName = "Työpaikkailmoitus")]
public class JobNoticeScriptable : ScriptableObject
{
    public string jobTitle;
    [TextArea]
    public string jobDescriptionOnNotice;

    //Boolit eri vaatimuksille? Esim jotain kortteja, hygienipassi? ajokortti? jotain muuta?
    public int workHoursPerDay;
    public float payByHour;
    public JobSiteScriptable jobSite;
    //Pitäisi varmaan tehdä joku osa-aika, kokoaika, provikkapalkka, muu tyyyppinen palkka simulaatio vielä...
}
