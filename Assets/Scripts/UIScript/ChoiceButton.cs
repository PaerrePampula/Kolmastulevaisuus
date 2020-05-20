using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
   // private GameEvent onButtonSelected;
    public TextMeshProUGUI buttonText;
    eventChoice choiceofThisButton;
    public ScriptableAction[] clickActions;
    RandomEventUI transformGrandParentScript; //Skripti, jossa tätä näppäintä on hallinnoitu = näppäimen event boksin skripti.
    // Start is called before the first frame update
    void Start()
    {
    }
    public void setChoiceText(string text)
    {
        buttonText.text = text; //Pyydetty teksti korvataan valintatekstillä.
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setButtonGrandParentScript(RandomEventUI choice)
    {
        transformGrandParentScript  = choice;
    }
    public void setButtonEventChoice(eventChoice choice)
    {
        choiceofThisButton = choice; //annetaan nappulalle se tieto, että mitä valintaa se edustaa.
        clickActions = choice.clickActions; //nappulalle annetaan myös kaikki actionit, jota scriptableobjectin actioneissa on määritelty.
    }
    public void AdvanceDialog()
    {
        transformGrandParentScript.AdvanceDialogTo(choiceofThisButton.nextDialog);
        if (clickActions.Length < 1)
        {
            return;
        }
        else
        {
            for (int i = 0; i < clickActions.Length; i++)
            {
                clickActions[i].PerformAction(); //Tässä samannimisiä performactioneita kutsutaan jokaisesta scriptableactionista, jota dialogin valintanäppäimeen on sidottu.
            }
        }

    }
}
