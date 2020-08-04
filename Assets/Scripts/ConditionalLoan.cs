using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConditionalLoan : MonoBehaviour
{

    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        if (Bank.Current.CanLoan && !button.interactable)
        {
            button.interactable = true;
        }
    }
}
