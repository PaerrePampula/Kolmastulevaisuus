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
        if (movementAllowed == true)
        {
            Movement();
        }

    }
    #endregion

    void Movement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit))
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
            if (playerNavMeshAgent.remainingDistance < 1)
            {
                hasAMoveCommand = false;
                OnMoveStopped?.Invoke();
                if (interactedObject != null)
                {
                    interactedObject.OnInteract();
                    interactedObject = null;
                }
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
