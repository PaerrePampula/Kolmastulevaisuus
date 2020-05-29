using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelfareSystem : MonoBehaviour
{
    private void Start()
    {
        /*System.DateTime newDate = new System.DateTime(2020, 10, 1);

        OpintoRaha raha = new OpintoRaha(DateTimeSystem.getCurrentDate(), newDate, true, typeOfSupport.OpintoTuki);

        currentPlayerSupports.Add(raha);*/
        //NÄMÄ ON DEBUG TUKIA, POISTA MYÖHEMMIN
    }
    List<IWelfareableSupport> currentPlayerSupports = new List<IWelfareableSupport>();
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
