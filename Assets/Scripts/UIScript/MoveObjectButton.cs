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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
