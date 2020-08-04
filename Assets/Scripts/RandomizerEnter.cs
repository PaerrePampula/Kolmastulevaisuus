using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerEnter : MonoBehaviour
{
    int chosenInt;
    Animator animator;
    [SerializeField]
    int possibleAnimations;
    private void OnEnable()
    {
        //Getataaan animator
        animator = GetComponent<Animator>();
        //Setataan random arvo, jonka avulla valitaan animaatio
        chosenInt = Random.Range(0, possibleAnimations);
        //Setataan saatu arvo animatorin integeriin.
        animator.SetInteger("ChosenInt", chosenInt);

    }
}
