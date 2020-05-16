using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Uusi satunnainen event", menuName = "Satunnainen event")]
public class RandomEventScriptable : ScriptableObject
{
    public eventText[] eventTexts;
}
[System.Serializable]
public class eventText
{
    [TextArea]
    public string eventDialog;

    public eventChoice[] eventDialogChoices;

}
[System.Serializable]
public class eventChoice
{
    public int nextDialog; //-1 on poistuminen.
    public string choiceDescriptor;

}
