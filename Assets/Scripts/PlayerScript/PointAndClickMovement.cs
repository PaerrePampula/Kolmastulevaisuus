using UnityEngine;
using UnityEngine.AI;

public class PointAndClickMovement : MonoBehaviour
{
    #region Fields
    NavMeshAgent playerNavMeshAgent;
    Vector3 playerNavMeshTarget;
    static bool movementAllowed;
    #endregion

    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        movementAllowed = true;
    }
    private void OnEnable()
    {
        UiGeneric.OnUIOpened += setMovementStatus;
    }
    private void OnDisable()
    {
        UiGeneric.OnUIOpened -= setMovementStatus;
    }
    // Update is called once per frame
    void Update()
    {
        if (movementAllowed)
        {
            Movement();
        }

    }
    #endregion

    void Movement()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                Debug.Log("Hello");
                playerNavMeshTarget = ray.GetPoint(distance);
                playerNavMeshAgent.SetDestination(playerNavMeshTarget);
            }
        }
    }
    public static bool getMovementStatus()
    {
        return movementAllowed;
    }
    public static void setMovementStatus(bool status)
    {
        movementAllowed = status;
    }
}
