using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovementDisabler : MonoBehaviour
{
    bool clickStatus;

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

}
