﻿using TMPro;
using UnityEngine;

public class ChoiceButton : BaseTrigger
{
    #region Fields
    // private GameEvent onButtonSelected;
    public TextMeshProUGUI buttonText;
    eventChoice choiceofThisButton;
    RandomEventUI thisRandomEventUI;
    public delegate void DialogAdvance(int index);
    public static event DialogAdvance OnDialogAdvance;
    #endregion
    public void Init(eventChoice choice, Transform newTransformParent, string buttonText, RandomEventUI ui)
    {
        gameObject.transform.parent.transform.SetParent(newTransformParent); //Parentiksi event UI
        choiceofThisButton = choice; //Näppäimen edustama event valinta
        eventTriggers = choice.clickActions; //Näppäimen valinnan edustamat ScriptableActionit
        flags = choice.firedFlags; //Näppäimen valinnan global flagit.
        customActions = choice.customRunTimeActions;
        randomizedChoiceActions = choice.randomizedChoiceCustomActions;
        thisRandomEventUI = ui;
        setChoiceText(buttonText);
    }
    public void setChoiceText(string text)
    {
        buttonText.text = text; //Pyydetty teksti korvataan valintatekstillä.
    }

    public void AdvanceDialog()
    {
        FireTriggersAndFlags(); //Basetrigger Method
        thisRandomEventUI.AdvanceDialogTo(choiceofThisButton.nextDialog);

    }
}
