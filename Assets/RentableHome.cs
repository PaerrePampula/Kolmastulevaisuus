﻿using UnityEngine;
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

    ExtrasOnRentableHomes[] rentExtras;
    GameObject prefab;
    HuoneKoko huoneKoko;
    VuokraTyyppi tyyppi;
    
    public RentableHome(RentableHomeScriptable scriptable)
    {
        address = scriptable.address;
        baseRentAmount = scriptable.baseRentAmount;
        shortFormDescription = scriptable.shortFormDescription;
        prefab = scriptable.prefab;
        waterCost = scriptable.waterCost;
        electricityCost = scriptable.electricityCost;
        homeInsurance = scriptable.homeInsurance;
        homeInsuranceNeeded = scriptable.needToHaveHomeInsurance;
        rentExtras = scriptable.extrasOnRentableHome;
        longFormDescription = scriptable.longFormDescription;
        perks = scriptable.perks;
        extrasDescription = scriptable.extrasDescription;
        huoneKoko = scriptable.huoneKoko;
        tyyppi = scriptable.vuokraTyyppi;
        size = scriptable.sizeInMetersSquared;
    }
    public string Address => address;
    public float BaseRent => baseRentAmount;
    public float WaterCost => waterCost;
    public float ElectricityCost => electricityCost;
    public float HomeInsuranceCost => homeInsurance;
    public float Size => size;
    public HuoneKoko RentableHuoneKoko => huoneKoko;
    public VuokraTyyppi RentableVuokraTyyppi => tyyppi;
}
