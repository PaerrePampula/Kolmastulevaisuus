using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Uusi satunnainen event", menuName = "Satunnainen event tekstiboksi")]
public class RandomEventScriptable : ScriptableObject //Tämä on melko yksiselitteinen, lista stringejä ja dialogivalintoja, sekä scriptableactioneita
{
    public FIRE_LOCATION fire_location;
    public eventText[] eventTexts; //Eventexts on säiliö eventin kuvaustekstille, sekä sen valintabokseille
}
[System.Serializable]
public class eventText
{
    [TextArea]
    public string eventDialog; //kuvausteksti tapahtumasta

    public eventChoice[] eventDialogChoices; //kaikki valinnat, jota tapahtuman aikana voi tehdä.

}
[System.Serializable]
public class eventChoice
{
    public int nextDialog; //-1 on poistuminen.
    public string choiceDescriptor;
    public ScriptableAction[] clickActions; //scriptableaction on tämänhetkinen implementointi tapahtumista, jotka tapahtuvat kun näppäintä painetaan.
    //Lisää tästä scriptableactionin alla.
    //Tälle on aivan varmasti parempikin ja selkeämpi tapa toteuttaa, mutta en toistaiseksi ole löytänyt /osannut tehdä sellaista.
}

