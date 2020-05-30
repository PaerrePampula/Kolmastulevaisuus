using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WelfareSystem : MonoBehaviour
{
    float maximumHouseholdGrossIncomeForAsuntotukiEligibilityInZone3WhenLivingAlone = 1608f;
    private void Start()
    {
        /*System.DateTime newDate = new System.DateTime(2020, 10, 1);

        OpintoRaha raha = new OpintoRaha(DateTimeSystem.getCurrentDate(), newDate, true, typeOfSupport.OpintoTuki);

        currentPlayerSupports.Add(raha);*/
        //NÄMÄ ON DEBUG TUKIA, POISTA MYÖHEMMIN
    }
    bool checkEligibilityForTypeOfSupport(typeOfSupport typeOfSupport)
    {
        bool check = false;
        switch (typeOfSupport)
        {
            case typeOfSupport.OpintoTuki:
                check =  DoesPlayerHaveSupportOfType(typeOfSupport.OpintoTuki) == true ? false : true; //Oletetaan että pelaaja opiskelee pelissä päätoimisesti, ja edistyy opinnoissa. Tätä voi toki myöhemmin muuttaa. Tsekataan vaan että onko jo tukea
                break;

            case typeOfSupport.YleinenAsumistuki:
                check = ((PlayerEconomy.CurrentPlayerEconomy.getAllIncomeSourceGrossTotals(1) <= maximumHouseholdGrossIncomeForAsuntotukiEligibilityInZone3WhenLivingAlone)
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
    List<IWelfareableSupport> currentPlayerSupports = new List<IWelfareableSupport>();
    bool DoesPlayerHaveSupportOfType(typeOfSupport QueryedTypeOfSupport)
    {
        var support = currentPlayerSupports.SingleOrDefault(support => support.GetTypeOfSupport() == QueryedTypeOfSupport);
        return (support != null) ? true : false;
    }
    private void OnEnable()
    {
        DateTimeSystem.OnMonthChange += givePlayerMonthlySupport;
    }
    private void OnDisable()
    {
        DateTimeSystem.OnMonthChange -= givePlayerMonthlySupport;
    }
    void givePlayerMonthlySupport()
    {
        foreach (IWelfareableSupport support in currentPlayerSupports)
        {
            if (support.checkValidityOfSupport() == true)
            {
                PaerToolBox.giveMoneyToPlayer(support.CalculatedSupport());
            }
            else
            {
                currentPlayerSupports.Remove(support);
                Debug.Log("Pelaajalta loppui tuki:" + support.CalculatedSupport() + " euroa. Ajalta" + support.getStartAndEndDate().Item1 + "-" + support.getStartAndEndDate().Item2);
                //Miksi tämän tuplen itemien returnaus returnaa molemmille itemeille itse sen päivän ja sitten tyhjän päiväarvon???? WTF
            }
        }
    }
}
