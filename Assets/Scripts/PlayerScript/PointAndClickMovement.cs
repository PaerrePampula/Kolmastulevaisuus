using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Dictionary<Transform, int> hiddenObjects = new Dictionary<Transform, int>();
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

        if (Physics.Raycast(transform.position + Vector3.up, Camera.main.transform.position - transform.position, out hit))
        {

            if (!hiddenObjects.Keys.Contains(hit.transform))
            {
                hiddenObjects.Add(hit.transform, hit.transform.gameObject.layer);
                hideObjectsOnFront();
            }
            Debug.Log("Infrontofplayer");
        }
        else if (hiddenObjects.Count > 0)
        {
            if (!Physics.Raycast(transform.position + Vector3.up, Camera.main.transform.position - transform.position, out hit))
            {
                unHideObjects();
            }
        }
    }
    #endregion
    void hideObjectsOnFront()
    {
        foreach (var item in hiddenObjects)
        {
            item.Key.gameObject.layer = 14;
            foreach (Transform tra in item.Key.transform)
            {
                tra.gameObject.layer = 14;
            }

        }
    }
    void unHideObjects()
    {
        foreach (var item in hiddenObjects)
        {
            item.Key.gameObject.layer = item.Value;
            foreach (Transform tra in item.Key.transform)
            {
                tra.gameObject.layer = item.Value;
            }

            hiddenObjects.Remove(item.Key);
        }
    }
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
                    //Debug.Log((playerNavMeshAgent.transform.position - interactedObject.transform.position).sqrMagnitude);
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
