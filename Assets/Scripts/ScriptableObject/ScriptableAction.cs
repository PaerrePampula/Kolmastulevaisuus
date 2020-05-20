using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{ //Tämä on siis pohjaclassi kaikille actioneille, jotka tapahtuvat dialogin edetessä.


    //Näitä voi määritellä mielin määrin missä tahansa classsissa, jos vaan inherittaat Tämän (JokuClass : ScriptableAction),
    //Sekä määrittelet [CreateAssetMenu(menuName ="Actions/JokuActionNimi")] classin nimen yläpuolelle.
    public abstract void PerformAction();
    //Tätä metodia käyttää kaikki actionit pohjanaan. Se korvataan jokaisessa actionissa omalla metodilla.
}
