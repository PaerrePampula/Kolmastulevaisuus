using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WelfareInfoWindow : MonoBehaviour
{
    public TextMeshProUGUI asumisText;
    public TextMeshProUGUI opintoText;
    public TextMeshProUGUI lainaText;
    void setWelfareInfo()
    {
        //Periaatteessa tän listingin vois kyl korvata jollakin muulla, kun kolminkertainen linq-query kyllä kuulostaa melko autistiselta ja tehottomalta
        //Welfaresystemiin vois merkata ehkä erilliset currentAsumiTuet jne, en tiiä onko se ollenkaan parempi sinänsä, lyhyitä listoja nämä queryää, ei ole välttämättä niin vakavaa
        var listing = WelfareSystem.Current.getWelfareListing();
        var asumisWelfare = listing.SingleOrDefault(welfare => welfare.GetTypeOfSupport() == typeOfSupport.YleinenAsumistuki);
        var opintoWelfare = listing.SingleOrDefault(welfare => welfare.GetTypeOfSupport() == typeOfSupport.OpintoTuki);
        var lainaWelfare = listing.SingleOrDefault(welfare => welfare.GetTypeOfSupport() == typeOfSupport.Opintolaina);
        if (asumisWelfare != null)
        {
            applyWelfareInfoOnString(asumisText, asumisWelfare);
        }
        if (opintoWelfare != null)
        {
            applyWelfareInfoOnString(opintoText, opintoWelfare);
        }
        if (lainaText != null)
        {
            applyWelfareInfoOnString(lainaText, lainaWelfare);
        }
    }
    void applyWelfareInfoOnString(TextMeshProUGUI target, IWelfareableSupport welfareable)
    {
        var text = welfareable.GetTypeOfSupport().ToString().Substring(0, 1) + welfareable.GetTypeOfSupport().ToString().Substring(1).ToLower();
        target.text  = text + "\n" + welfareable.CalculatedSupport() + " euroa per kuukausi.\n"
        + "Tuki myönnetty ajalle " + DateTimeSystem.returnDayYearMonth(welfareable.getStartAndEndDate().Item1) + "-" +
        DateTimeSystem.returnDayYearMonth(welfareable.getStartAndEndDate().Item2);
        if (welfareable.GetTypeOfSupport() == typeOfSupport.Opintolaina)
        {
            target.text += "\nTämä on vain lainan valtiontakaus, laina ei tule automaattisesti kuukausittain, hae laina pankilta sopivissa erissä, tai kokonaisena";
        }
    }
    private void OnEnable()
    {
        setWelfareInfo();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
