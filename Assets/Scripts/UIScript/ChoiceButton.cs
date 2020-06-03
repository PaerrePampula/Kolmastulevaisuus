using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    #region Fields
    // private GameEvent onButtonSelected;
    public TextMeshProUGUI buttonText;
    eventChoice choiceofThisButton;
    public ScriptableAction[] clickActions;
    RandomEventUI transformGrandParentScript; //Skripti, jossa tätä näppäintä on hallinnoitu = näppäimen event boksin skripti.
    public Flag[] flags;
    #endregion


    public void setChoiceText(string text)
    {
        buttonText.text = text; //Pyydetty teksti korvataan valintatekstillä.
    }
    public void setButtonGrandParentScript(RandomEventUI choice)
    {
        transformGrandParentScript = choice;
    }
    public void setButtonEventChoice(eventChoice choice)
    {
        choiceofThisButton = choice; //annetaan nappulalle se tieto, että mitä valintaa se edustaa.
        clickActions = choice.clickActions; //nappulalle annetaan myös kaikki actionit, jota scriptableobjectin actioneissa on määritelty.
        flags = choice.firedFlags;
    }
    public void AdvanceDialog()
    {
        foreach (Flag item in flags)
        {
            item.FireFlag();
        }
        transformGrandParentScript.AdvanceDialogTo(choiceofThisButton.nextDialog);
   
        for (int i = 0; i < clickActions.Length; i++)
        {
            clickActions[i].PerformAction(); //Tässä samannimisiä performactioneita kutsutaan jokaisesta scriptableactionista, jota dialogin valintanäppäimeen on sidottu.

        }



    }
}
