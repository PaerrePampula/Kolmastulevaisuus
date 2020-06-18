using UnityEngine;
using System.Collections;

public class PlacementHelper : MonoBehaviour
{
    static bool placing = false;
    static bool moving = false;
    int moveClickCount;
    GameObject placingObject;
    Bounds meshBounds;
    BuyObject currentBuyObject;
    public LayerMask placementLayer;
    public LayerMask furnitureLayer;


    public delegate void PurchaseCall(BuyObject gameObject);
    public static event PurchaseCall OnObjectPurchase;
    public delegate void PurchaseSatisfactionCall(float satisfaction);
    public static event PurchaseSatisfactionCall OnObjectSatisfaction;
    public delegate void BeginPlacementCall(bool isBegin);
    public static event BeginPlacementCall OnPlacementInteract;

    public static bool GetPlacing()
    {
        return placing;
    }

    public static void SetPlacing(bool value)
    {
        placing = value;
    }
    public static bool GetMoving()
    {
        return moving;
    }

    public static void SetMoving(bool value)
    {
        moving = value;
    }

    private void OnEnable()
    {
        BuyObjectButton.OnObjectClicked += startPlacement;
        MoveObjectButton.OnMoveClicked += startMovement;
    }
    private void OnDisable()
    {
        BuyObjectButton.OnObjectClicked -= startPlacement;
        MoveObjectButton.OnMoveClicked -= startMovement;
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
        OnPlacementInteract?.Invoke(true);
        meshBounds = placingObject.GetComponent<Collider>().bounds;
        PointAndClickMovement.setMovementStatus(false);
        MainCanvas.mainCanvas.freezeOverride = true;
        if (placingObject.GetComponent<WorldInteractive>() != null)
        {
            placingObject.GetComponent<WorldInteractive>().beingMoved = true;
        }
    }
    void movePlacement()
    {

        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
       
        if (Physics.SphereCast(Camera.main.ScreenPointToRay(mousePos), meshBounds.extents.sqrMagnitude*0.1f, out hit, Mathf.Infinity, furnitureLayer))
        {
            return;

        }
        if (Physics.SphereCast(Camera.main.ScreenPointToRay(mousePos), meshBounds.extents.magnitude*0.1f, out hit, Mathf.Infinity, placementLayer))
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
    void placePlacement(bool buying)
    {
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.SphereCast(Camera.main.ScreenPointToRay(mousePos), 1, out hit, Mathf.Infinity, placementLayer))
            {
                if (placingObject.GetComponent<WorldInteractive>() != null)
                {
                    placingObject.GetComponent<WorldInteractive>().beingMoved = false;
                }
                OnPlacementInteract?.Invoke(false);
                placingObject.layer = 12;
                if (buying)
                {
                    callForPurchase();
                    Debug.Log("HÄr");
                }

                placingObject = null;
                SetPlacing(false);
                SetMoving(false);
                PointAndClickMovement.setMovementStatus(true);
                MainCanvas.mainCanvas.freezeOverride = false;
            }

        }
    }
    void cancelPlacement()
    {
        if (Input.GetMouseButtonDown(3))
        {
            SetPlacing(false);
            if (placingObject != null)
            {
                Destroy(placingObject);
                currentBuyObject = null;
            }
            OnPlacementInteract?.Invoke(false);
        }
    }
    void clickOnPlacement()
    {
        if (Input.GetMouseButtonDown(0) && placingObject == null)
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit, Mathf.Infinity, furnitureLayer))
            {
                placingObject = hit.transform.gameObject;
                meshBounds = placingObject.GetComponent<Collider>().bounds;
                placingObject.layer = 0;

            }


        }
    }
    void startMovement()
    {
        SetMoving(true);
        placingObject = null;

        PointAndClickMovement.setMovementStatus(false);
        MainCanvas.mainCanvas.freezeOverride = true;
        if (placingObject.GetComponent<WorldInteractive>() != null)
        {
            placingObject.GetComponent<WorldInteractive>().beingMoved = true;
        }
    }
    void callForPurchase()
    {
        OnObjectPurchase?.Invoke(currentBuyObject);
        OnObjectSatisfaction?.Invoke(currentBuyObject.SatisfactionGain);

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
            placePlacement(true);
        }
        if (GetMoving())
        {
            clickOnPlacement();
            if (placingObject != null)
            {
                movePlacement();
                rotatePlacement();
                placePlacement(false);
            }

        }
    }
}
