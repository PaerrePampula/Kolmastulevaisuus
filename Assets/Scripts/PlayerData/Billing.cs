using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Billing : MonoBehaviour
{
    float monthPaymentForTrains = 0;
    List<Bill> listOfBills;
    private void OnEnable()
    {
        GameEventSystem.RegisterListener(Event_Type.CREATE_BILLING, createBillFromEvent);
        Bill.onBillingCreate += addBill;
    }
    private void OnDisable()
    {
        GameEventSystem.UnRegisterListener(Event_Type.CREATE_BILLING, createBillFromEvent);
        Bill.onBillingCreate -= addBill;
    }
    private void Start()
    {
        listOfBills = new List<Bill>();
        monthPaymentForTrains = ConfigFileReader.getValue("SeasonTicketSmallTripPrice");
        if (PlayerDataHolder.playerHome.CloseToSchool == false)
        {
            Bill trainPayment = new Bill("Juna kausilippu - 30 päivää", monthPaymentForTrains);
        }
        Bill homeOtherPayments = new Bill("Kodin lyhytkestoiset tarvikkeet", 10f);
        Bill homeHygiene = new Bill("Henkilökohtainen hygienia", 25f);

    }
    void addBill(Bill bill)
    {
        listOfBills.Add(bill);
    }
    void createBillFromEvent(EventInfo info)
    {
        PurchaseInfo billInfo = (PurchaseInfo)info;
        Bill bill = new Bill(billInfo.purchaseName, billInfo.purchaseCost,0, billInfo.singleuse);
        addBill(bill);
    }
}
