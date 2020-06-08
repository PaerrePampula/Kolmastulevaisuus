using UnityEngine;

public abstract class ScriptableAction : ScriptableObject, IActionable
{ //Tämä on siis pohjaclassi kaikille actioneille, jotka tapahtuvat dialogin edetessä.
    [Tooltip("Tätä ei tarvii kaikissa erikseen vaihtaa, vilkaise scriptableista että tarviiko" +
        "\nHyvin merkitsevää että laitetaan oikein, antaa muuten invalid cast erroreita"
        + "\nUusia actioneita luodessa muista myös aina laittaa Eventinfoon uusi event " +
        "infoa inherittaava tieto, joka menee actionin läpi")]
    [Header("Hoveraamalla lisätietoa...")]
    public Event_Type event_Type;
    //Näitä voi määritellä mielin määrin missä tahansa classsissa, jos vaan inherittaat Tämän (JokuClass : ScriptableAction),
    //Sekä määrittelet [CreateAssetMenu(menuName ="Actions/JokuActionNimi")] classin nimen yläpuolelle.
    [Tooltip("Kuvaus vain debugille, että mitä actionissa tapahtuu")]
    public string description;

    public Event_Type thisEvent_Type { get => event_Type; set => event_Type = value; }
    public string Description { get => description; set => description = value; }

    public abstract void PerformAction();
    //Tätä metodia käyttää kaikki actionit pohjanaan. Se korvataan jokaisessa actionissa omalla metodilla.
}
