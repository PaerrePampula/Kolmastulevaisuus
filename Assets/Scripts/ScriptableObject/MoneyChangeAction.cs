using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Actions/MoneyChange")]
public class MoneyChangeAction : ScriptableAction //Tämä on scriptableaction esimerkki, näitä käytetään dialogeissa, kun on tarve nostaa joku actioni valinnan perusteella.
{
    public float amount;
    private GameObject kukkaro;

    public override void PerformAction(float? changedFloat, int? changedint = null, GameObject chosenObject = null)
    {
        Debug.Log("Pelaaja sai rahaa!, määrä on " + amount);
        kukkaro = GameObject.Find("EventController");
        kukkaro.GetComponent<Kukkaro>().setMoney(amount);
    }
}
