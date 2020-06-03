﻿using TMPro;
using UnityEngine;

public class ChoiceButton : BaseTrigger
{
    #region Fields
    // private GameEvent onButtonSelected;
    public TextMeshProUGUI buttonText;
    eventChoice choiceofThisButton;
    public delegate void DialogAdvance(int index);
    public static event DialogAdvance OnDialogAdvance;
    #endregion

    public void setChoiceText(string text)
    {
        buttonText.text = text; //Pyydetty teksti korvataan valintatekstillä.
    }
    public void setButtonEventChoice(eventChoice choice)
    {
        choiceofThisButton = choice; //annetaan nappulalle se tieto, että mitä valintaa se edustaa.
        eventTriggers = choice.clickActions; //nappulalle annetaan myös kaikki actionit, jota scriptableobjectin actioneissa on määritelty.
        flags = choice.firedFlags;
    }
    public void AdvanceDialog()
    {
        FireTriggersAndFlags();

        OnDialogAdvance?.Invoke(choiceofThisButton.nextDialog);
    }
}
