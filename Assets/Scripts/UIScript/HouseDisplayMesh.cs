using UnityEngine;
using System.Collections;

public class HouseDisplayMesh : MonoBehaviour //low prio, mutta pitäis yhdistää BuyObjectRendMeshin kaa
{

    GameObject displayObject;
    private void OnEnable()
    {
        RentableUI.onUiOpen += instantiateNewHoverObject;
    }
    private void OnDisable()
    {
        RentableUI.onUiOpen -= instantiateNewHoverObject;
    }
    void instantiateNewHoverObject(GameObject go)
    {
        if (displayObject != null)
        {
            Destroy(displayObject);
        }
        displayObject = Instantiate(go, transform);
        displayObject.transform.localPosition = Vector3.zero;
        displayObject.transform.rotation = Quaternion.identity;

    }
}
