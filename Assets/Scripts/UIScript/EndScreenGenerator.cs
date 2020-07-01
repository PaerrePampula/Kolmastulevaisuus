using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenGenerator : MonoBehaviour
{
    [SerializeField]
    Transform endScreen;

    private void OnEnable()
    {
        EndScreenFadeOut.OnEnd += startEnd;
    }
    private void OnDisable()
    {
        EndScreenFadeOut.OnEnd -= startEnd;
    }

    void startEnd()
    {
        endScreen.gameObject.SetActive(true);
    }
}
