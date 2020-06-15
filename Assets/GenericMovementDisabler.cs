using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovementDisabler : MonoBehaviour
{
    bool clickStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setMovement()
    {
        switch (clickStatus)
        {
            case true:
                transform.SetParent(MainCanvas.mainTransform.transform);
                clickStatus = false;
                break;
            case false:
                transform.SetParent(MainCanvas.mainCanvas.parentOfNewTransforms);
                clickStatus = true;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
