using UnityEngine;
using System.Collections;

public class PlacementHelper : MonoBehaviour
{
    static bool placing = false;
    GameObject placingObject;
    BuyObject currentBuyObject;
    public LayerMask placementLayer;
    public delegate void PurchaseCall(BuyObject gameObject);
    public static event PurchaseCall OnObjectPurchase;

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
        currentBuyObject = buyObject;

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
            callForPurchase();

        }
    }
    void cancelPlacement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetPlacing(false);
            if (placingObject != null)
            {
                Destroy(placingObject);
                currentBuyObject = null;
            }
        }
    }
    void callForPurchase()
    {
        OnObjectPurchase?.Invoke(currentBuyObject);
        PlayerEconomy.createPurchase(currentBuyObject.BuyName, -currentBuyObject.BuyValue);
        currentBuyObject = null;


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
            cancelPlacement();
            movePlacement();
            rotatePlacement();
            placePlacement();
        }
    }
}
