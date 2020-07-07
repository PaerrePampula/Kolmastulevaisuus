using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickMovement : MonoBehaviour
{
    #region Fields
    NavMeshAgent playerNavMeshAgent;
    static Vector3 playerNavMeshTarget;
    static bool movementAllowed;
    bool hasAMoveCommand;
    WorldInteractive interactedObject;
    public delegate void MovePlayer(Vector3 position);
    public static event MovePlayer OnMoveStart;
    public delegate void MovedPlayer();
    public static event MovedPlayer OnMoveStopped;
    public LayerMask IgnoreMe;
    #endregion

    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();

    }
    private void OnEnable()
    {
        MainCanvas.OnFreeze += setMovementStatus;

        
    }
    private void OnDisable()
    {
        MainCanvas.OnFreeze -= setMovementStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementAllowed == true && PlacementHelper.GetPlacing() == false)
        {
            Movement();
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Camera.main.transform.position - transform.position, out hit))
        {

            Debug.Log("Infrontofplayer");
        }
    }
    #endregion

    void Movement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit, Mathf.Infinity, ~IgnoreMe))
            {
                hasAMoveCommand = true;
                playerNavMeshTarget = hit.point;
                playerNavMeshAgent.SetDestination(playerNavMeshTarget);
                OnMoveStart?.Invoke(playerNavMeshAgent.destination);
                if(hit.transform.gameObject.GetComponent<WorldInteractive>() != null)
                {
                    interactedObject = hit.transform.gameObject.GetComponent<WorldInteractive>();
                }
            }


        }
        if ((hasAMoveCommand == true) && (playerNavMeshAgent.velocity == Vector3.zero) && (!playerNavMeshAgent.pathPending))
        {
            if (playerNavMeshAgent.remainingDistance < playerNavMeshAgent.stoppingDistance)
            {

                if ((interactedObject != null))
                {
                    Debug.Log((playerNavMeshAgent.transform.position - interactedObject.transform.position).sqrMagnitude);
                    interactedObject.OnInteract();
                    interactedObject = null;
                }
                hasAMoveCommand = false;
                OnMoveStopped?.Invoke();
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
