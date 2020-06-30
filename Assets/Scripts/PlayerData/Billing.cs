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
        GameEventSystem.RegisterListener(Event_Type.CREATE_BILLING, createBillFromEvent);
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
    void createBillFromEvent(EventInfo info)
    {
        PurchaseInfo billInfo = (PurchaseInfo)info;
        Bill bill = new Bill(billInfo.purchaseName, billInfo.purchaseCost);
        addBill(bill);
    }
}
