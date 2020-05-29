using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelfareSystem : MonoBehaviour
{
    private void Start()
    {
        System.DateTime newDate = new System.DateTime(2020, 10, 1);
        System.DateTime newDate2 = new System.DateTime(2021, 10, 1);
        OpintoRaha raha = new OpintoRaha(DateTimeSystem.getCurrentDate(), newDate, true);
        OpintoRaha raha2 = new OpintoRaha(DateTimeSystem.getCurrentDate(), newDate2, true);
        currentPlayerSupports.Add(raha);
        currentPlayerSupports.Add(raha2);
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
                //Tämä korvataan myöhemmin paremmalla rakenteella
                FloatChangeInfo valueChangeAction = new FloatChangeInfo();
                valueChangeAction.changeofFloat = support.CalculatedSupport();
                Debug.Log("Pelaaja sai rahaa!, määrä on " + valueChangeAction.changeofFloat);
                GameEventSystem.Current.DoEvent(
                    Event_Type.FLOAT_CHANGE,
                    valueChangeAction
                    );
            }
            else
            {
                currentPlayerSupports.Remove(support);
            }
        }
    }
}
