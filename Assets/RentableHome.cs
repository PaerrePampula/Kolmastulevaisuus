using UnityEngine;
using System.Collections;

public class RentableHome
{
    string address;
    float baseRentAmount;
    float waterCost;
    float electricityCost;
    float homeInsurance;
    bool homeInsuranceNeeded;
    (string, float)[] extrasCost;
    string longFormDescription;
    string perks;
    string shortFormDescription;
    string extrasDescription;
    GameObject prefab;
    public RentableHome(string rentAddress, float baseRent, string shortDescription, GameObject rentHomePrefab , float rentWater = 0,
        float rentElectricity = 0, float rentInsurance = 0, bool doesNeedToHaveInsurance = false,
        (string,float)[] extrasRentCost = null, string rentLongFormDescription = "", string Rentperks = "", 
        string RentextrasDescription = "")
    {
        address = rentAddress;
        baseRentAmount = baseRent;
        shortFormDescription = shortDescription;
        prefab = rentHomePrefab;
        waterCost = rentWater;
        electricityCost = rentElectricity;
        homeInsurance = rentInsurance;
        homeInsuranceNeeded = doesNeedToHaveInsurance;
        extrasCost = extrasRentCost;
        longFormDescription = rentLongFormDescription;
        perks = Rentperks;
        extrasDescription = RentextrasDescription;
    }
}
