using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    eventChoice choiceofThisButton;
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
        choiceofThisButton = choice;
    }
    public void AdvanceDialog()
    {
        transformGrandParentScript.AdvanceDialogTo(choiceofThisButton.nextDialog);
    }
}
