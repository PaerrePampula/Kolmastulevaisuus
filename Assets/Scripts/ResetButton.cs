using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public delegate void Resetting();
    public static event Resetting onReset;
    public void onClick()
    {
        onReset.Invoke();
        SceneManager.LoadScene(0);

    }
}
