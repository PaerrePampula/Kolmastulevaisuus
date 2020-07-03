using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenGenerator : MonoBehaviour
{
    [SerializeField]
    Transform endScreen;
    [SerializeField]
    GameObject endScreenCamera;

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
        Camera.main.gameObject.SetActive(false);
        endScreenCamera.gameObject.SetActive(true);
        endScreenCamera.gameObject.tag = "MainCamera";
        MainCanvas.mainCanvas.gameObject.SetActive(false);
        
    }
}
