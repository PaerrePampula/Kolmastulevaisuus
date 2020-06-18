using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizingButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void OnMouseEnter()
    {
        animator.SetTrigger("ResizeTrigger");
    }
    void OnMouseExit()
    {
        animator.SetTrigger("ResizeTrigger");
    }

}
