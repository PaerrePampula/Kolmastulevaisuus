using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    static Transform canvasTransform;
    static private MainCanvas _currentMainCanvas;
    static public MainCanvas mainCanvas
    {
        get
        {
            if (_currentMainCanvas == null)
            {
                _currentMainCanvas = FindObjectOfType<MainCanvas>();
            }
            return _currentMainCanvas;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Transform getMainCanvasTransform()
    {
        return canvasTransform;
    }
}
