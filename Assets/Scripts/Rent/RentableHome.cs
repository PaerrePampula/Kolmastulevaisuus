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
    float size;
    string longFormDescription;
    string perks;
    string shortFormDescription;
    string extrasDescription;
    bool closeToSchool;
    string rentableScene = "StudioHomeNo1";
    ExtrasOnRentableHomes[] rentExtras;
    string prefabForDisplay;
    HuoneKoko huoneKoko;
    VuokraTyyppi tyyppi;


    public string Address => address;
    public float BaseRent => baseRentAmount;
    public float WaterCost => waterCost;
    public float ElectricityCost => electricityCost;
    public float HomeInsuranceCost => homeInsurance;
    public float Size => size;
    public string LongDescription => longFormDescription;
    public string ShortDescription => shortFormDescription;
    public HuoneKoko RentableHuoneKoko => huoneKoko;
    public VuokraTyyppi RentableVuokraTyyppi => tyyppi;

    public string PrefabForDisplay { get => prefabForDisplay; set => prefabForDisplay = value; }
    public bool CloseToSchool { get => closeToSchool; set => closeToSchool = value; }
    public ExtrasOnRentableHomes[] RentExtras { get => rentExtras; set => rentExtras = value; }

    public string GetRentableScene()
    {
        return rentableScene;
    }

    public void SetRentableScene(string value)
    {
        rentableScene = value;
    }

    public RentableHome(RentableHomeScriptable scriptable)
    {
        address = scriptable.address;
        baseRentAmount = scriptable.baseRentAmount;
        shortFormDescription = scriptable.shortFormDescription;
        SetRentableScene(scriptable.rentableScene);
        waterCost = scriptable.waterCost;
        electricityCost = scriptable.electricityCost;
        homeInsurance = scriptable.homeInsurance;
        homeInsuranceNeeded = scriptable.needToHaveHomeInsurance;
        RentExtras = scriptable.extrasOnRentableHome;
        longFormDescription = scriptable.longFormDescription;
        perks = scriptable.perks;
        extrasDescription = scriptable.extrasDescription;
        huoneKoko = scriptable.huoneKoko;
        tyyppi = scriptable.vuokraTyyppi;
        size = scriptable.sizeInMetersSquared;
        prefabForDisplay = scriptable.displayPrefab;
        closeToSchool = scriptable.closeEnoughToSchoolToWalk;
    }
    public float getRentTotalForAMonth()
    {
        float rentAmount = 0;
        rentAmount += baseRentAmount;
        rentAmount += waterCost;
        foreach (var extras in RentExtras)
        {
            rentAmount += extras.extraCostPerMonth;
        }
        return rentAmount;
    }
}
