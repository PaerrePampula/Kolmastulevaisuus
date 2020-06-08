using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WelfareSystem : MonoBehaviour //Tää säilöö muutaman scriptableactionin niin pidän sen inherittaamassa monoa.
    //Vois periaatteessa siirtääkin johonkin muuhun järjestelmään, mutta melko pienen prioriteetin homma
{
    #region Fields

    float maximumHouseholdGrossIncomeForAsuntotukiEligibilityInZone3WhenLivingAlone;
    List<IWelfareableSupport> currentPlayerSupports = new List<IWelfareableSupport>();
    static private WelfareSystem _current;
    static public WelfareSystem Current
    {
        get
        {
            if (_current == null)
            {
                 _current = FindObjectOfType<WelfareSystem>();
            }
            return _current;
        }
    }

    #endregion

    #region MonobehaviourDefaults
    private void Start()
    {
        maximumHouseholdGrossIncomeForAsuntotukiEligibilityInZone3WhenLivingAlone = ConfigFileReader.getValue("maxGrossForHouseHoldAsumistuki");
        GameEventSystem.RegisterListener(Event_Type.PLAYER_WANTS_WELFARE, OnAWelfareApply);

    }
    private void OnEnable()
    {
        DateTimeSystem.OnMonthChange += givePlayerMonthlySupport;
        IncomeSource.OnNewIncomeRegister += checkEligiblityOnNewIncomeArrival;
    }
    private void OnDisable()
    {
        DateTimeSystem.OnMonthChange -= givePlayerMonthlySupport;
        IncomeSource.OnNewIncomeRegister -= checkEligiblityOnNewIncomeArrival;
    }
    #endregion

    bool checkEligibilityForTypeOfSupport(typeOfSupport typeOfSupport) //TODO: Kuntoutusraha checkit, varmista jos opintotuki vaatii enemmän checkkejä, samoin opintolaina.
    {
        bool check = false;
        switch (typeOfSupport)
        {
            case typeOfSupport.OpintoTuki:
                check = DoesPlayerHaveSupportOfType(typeOfSupport.OpintoTuki) == true ? false : true; //Oletetaan että pelaaja opiskelee pelissä päätoimisesti, ja edistyy opinnoissa. Tätä voi toki myöhemmin muuttaa. Tsekataan vaan että onko jo tukea
                break;

            case typeOfSupport.YleinenAsumistuki:
                check = ((PlayerEconomy.getAllIncomeSourceGrossTotals(1) <= maximumHouseholdGrossIncomeForAsuntotukiEligibilityInZone3WhenLivingAlone)
                    && DoesPlayerHaveSupportOfType(typeOfSupport.YleinenAsumistuki) == false) ? true : false;
                break;
            //Jos pelaajan tulot bruttona ovat isommat kuin annettu maksimimäärä, niin pelaaja ei voi saada asuntotukea
            //Tsekataan kanssa että onko jo tukea

            case typeOfSupport.Opintolaina:
                check = ((DoesPlayerHaveSupportOfType(typeOfSupport.OpintoTuki) == true) && (DoesPlayerHaveSupportOfType(typeOfSupport.Opintolaina) == false)) ? true : false;
                break;

            case typeOfSupport.Kuntoutusraha:
                check = ((DoesPlayerHaveSupportOfType(typeOfSupport.Kuntoutusraha) == true) ? false : true);
                break;
        }
        return check;
    }


    bool DoesPlayerHaveSupportOfType(typeOfSupport QueryedTypeOfSupport)
    {
        var support = currentPlayerSupports.SingleOrDefault(x => x.GetTypeOfSupport() == QueryedTypeOfSupport);
        return (support != null) ? true : false;
    }

    void givePlayerMonthlySupport()
    {
        foreach (IWelfareableSupport support in currentPlayerSupports)
        {
            if (support.checkValidityOfSupport() == true)
            {
                if (support.isAMonthlySupport == false) return; //pelaaja ei saa tätä tukea kuukausittain jos se ei ole, noh kuukausittainen.
                PaerToolBox.changePlayerMoney(support.CalculatedSupport());
            }
            else
            {
                removePlayerSupport(support);
            }
        }
    }
    void checkEligiblityOnNewIncomeArrival()
    {
        var asuntoTuki = currentPlayerSupports.SingleOrDefault(support => support.GetTypeOfSupport() == typeOfSupport.YleinenAsumistuki);
        if (asuntoTuki != null && PlayerEconomy.getAllIncomeSourceGrossTotals(1) > maximumHouseholdGrossIncomeForAsuntotukiEligibilityInZone3WhenLivingAlone)
        {
            Flag flag = new Flag("PLAYER_HAS_TOO_MUCH_INCOME", 0, true);
            flag.FireFlag();
            removePlayerSupport(asuntoTuki);
        }

    }
    void removePlayerSupport(IWelfareableSupport support)
    {
        currentPlayerSupports.Remove(support);
        Debug.Log("Pelaajalta loppui tuki:" + support.CalculatedSupport() + " euroa. Ajalta" + support.getStartAndEndDate().Item1 + "-" + support.getStartAndEndDate().Item2);
        //Miksi tämän tuplen itemien returnaus returnaa molemmille itemeille itse sen päivän ja sitten tyhjän päiväarvon???? WTF
        //Protip: tajusin että se 0.00.00 palautus onkin kellonaika, lol
    }
    void OnAWelfareApply(EventInfo eventInfo)
    {
        WelfareApplyFormInfo welfareApplyFormInfo = (WelfareApplyFormInfo)eventInfo;
        bool canApplyThisSupport = (checkEligibilityForTypeOfSupport(welfareApplyFormInfo.typeofWelfare) == true) ? true : false; //Jos logiikka vain sallii sen, pelaaja saa hakea tätä tukea juuri nyt.
        if (canApplyThisSupport)
        {
            switch (welfareApplyFormInfo.typeofWelfare)
            {
                case typeOfSupport.OpintoTuki:
                    OpintoRaha raha = new OpintoRaha(DateTimeSystem.getCurrentDate(), welfareApplyFormInfo.timeWelfareAppliedFor.Item2, true, typeOfSupport.OpintoTuki);
                    currentPlayerSupports.Add(raha);
                    Flag flag = new Flag("OPINTOTUKI_ACCEPTED", 0, true);
                    flag.FireFlag();
                    break;
                case typeOfSupport.YleinenAsumistuki:
                    AsumisTuki asumisTuki = new AsumisTuki(DateTimeSystem.getCurrentDate(), welfareApplyFormInfo.timeWelfareAppliedFor.Item2, true, typeOfSupport.YleinenAsumistuki);
                    Flag flag2 = new Flag("ASUMISTUKI_ACCEPTED", 0, true);
                    flag2.FireFlag();
                    currentPlayerSupports.Add(asumisTuki);
                    foreach (var item in currentPlayerSupports)
                    {
                        Debug.Log(item.CalculatedSupport());
                    }

                    break;
                case typeOfSupport.Opintolaina:
                    OpintoLaina opintoLaina = new OpintoLaina(DateTimeSystem.getCurrentDate(), welfareApplyFormInfo.timeWelfareAppliedFor.Item2, false, typeOfSupport.Opintolaina);
                    Flag flag3 = new Flag("OPINTOLAINA_ACCEPTED", 0, true);
                    flag3.FireFlag();
                    currentPlayerSupports.Add(opintoLaina);
                    break;
                case typeOfSupport.Kuntoutusraha:
                    break;
                default:
                    break;
            }
        }
        else
        {
            Flag flag = new Flag("PLAYER_CANT_APPLY", 0, true);
            flag.FireFlag();
        }

    }
    public List<IWelfareableSupport> getWelfareListing()
    {
        return currentPlayerSupports;
    }

}
