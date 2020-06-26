using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Billing : MonoBehaviour
{
    float monthPaymentForTrains = 0;
    List<Bill> listOfBills = new List<Bill>();
    private void OnEnable()
    {
        Bill.onBillingCreate += addBill;
    }
    private void Start()
    {
        monthPaymentForTrains = ConfigFileReader.getValue("SeasonTicketSmallTripPrice");
        if (PlayerDataHolder.playerHome.CloseToSchool == false)
        {
            Bill trainPayment = new Bill("Juna kausilippu - 30 päivää", monthPaymentForTrains);
        }

    }
    void addBill(Bill bill)
    {
        listOfBills.Add(bill);
    }
}
