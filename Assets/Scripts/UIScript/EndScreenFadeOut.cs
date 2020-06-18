using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenFadeOut : MonoBehaviour
{
    public delegate void EndScreenTime();
    public static event EndScreenTime OnEnd;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void EndScreen()
    {
        animator.speed = 0;
        OnEnd?.Invoke();
    }
}
