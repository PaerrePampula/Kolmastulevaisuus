using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectButton : MonoBehaviour
{
    public delegate void ClickedMove();
    public static event ClickedMove OnMoveClicked;
    public void OnClick()
    {
        OnMoveClicked?.Invoke();
    }

}
