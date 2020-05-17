using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kukkaro : MonoBehaviour
{
    //Tämä on prototyyppi lopullisesta pelaajan taloudenlaskemisen simuloinnista, toistaiseksi melko yksinkertainen
    float money; //Yksiselitteisesti pelaajan rahatilanne.
    public delegate void IncreaseAction(float amount); 
    public static event IncreaseAction OnIncrease;
    //Delegatesta ja eventistä. Näillä kukkaro saa lähetettyä rahanmuutoksesta viestin kaikille onincreasen tilanneille 
    //classeille viestin siitä, että rahatilanne on muuttunut, delegaten ja eventin avulla näitä tilaajia ei tarvitse
    //erikseen unityn editorissä määritellä, eli toisinsanoen pelaajan kukkaron ei tarvitse tietää, kenelle tätä viestiä lähetetään.
    public void setMoney(float increase)
    {
        money += increase;
        if (OnIncrease != null) //Jos tapahtumalle OnIncrease on tilaajia, lähetä viesti
        {
            OnIncrease(money);
        }
    }
    public void getMoney()
    {
        Debug.Log(money);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
