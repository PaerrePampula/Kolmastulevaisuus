using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{ //Tämä on siis pohjaclassi kaikille actioneille, jotka tapahtuvat dialogin edetessä.

    public Event_Type Event_Type;
    //Näitä voi määritellä mielin määrin missä tahansa classsissa, jos vaan inherittaat Tämän (JokuClass : ScriptableAction),
    //Sekä määrittelet [CreateAssetMenu(menuName ="Actions/JokuActionNimi")] classin nimen yläpuolelle.
    public string Description;
    public abstract void PerformAction();
    //Tätä metodia käyttää kaikki actionit pohjanaan. Se korvataan jokaisessa actionissa omalla metodilla.
}
