using UnityEngine;
using System.Collections;

public class PlacementHelper : MonoBehaviour
{
    static bool placing = false;
    GameObject placingObject;
    public LayerMask placementLayer;

    public static bool GetPlacing()
    {
        return placing;
    }

    public static void SetPlacing(bool value)
    {
        placing = value;
    }

    private void OnEnable()
    {
        BuyObjectButton.OnObjectClicked += startPlacement;
    }
    private void OnDisable()
    {
        BuyObjectButton.OnObjectClicked -= startPlacement;
    }
    void startPlacement(BuyObject buyObject)
    {
        if (placingObject != null)
        {
            Destroy(placingObject);
        }
        placingObject = Instantiate(buyObject.GetBuyObjectScriptable().prefab);
        placingObject.transform.rotation = Quaternion.Euler(0, 130, 0);
        SetPlacing(true);

    }
    void movePlacement()
    {

        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit, Mathf.Infinity, placementLayer))
        {
            placingObject.transform.position = hit.point;
        }
    }
    void rotatePlacement()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        {
            if (scroll < 0)
            {
                placingObject.transform.rotation = Quaternion.Euler(placingObject.transform.rotation.eulerAngles.x, placingObject.transform.rotation.eulerAngles.y+90, placingObject.transform.rotation.z);
            }
            else if (scroll > 0)
            {
                placingObject.transform.rotation = Quaternion.Euler(placingObject.transform.rotation.eulerAngles.x, placingObject.transform.rotation.eulerAngles.y - 90, placingObject.transform.rotation.z);
            }
        }
    }
    void placePlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetPlacing(false);
            placingObject = null;

        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetPlacing())
        {
            movePlacement();
            rotatePlacement();
            placePlacement();
        }
    }
}
