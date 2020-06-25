using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameStateHandler.OnGameEnd += startEndScreen;
    }
    private void OnDisable()
    {
        GameStateHandler.OnGameEnd -= startEndScreen;
    }
    void startEndScreen()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
