using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickMovement : MonoBehaviour
{
    NavMeshAgent playerNavMeshAgent;
    Vector3 playerNavMeshTarget;
    static bool movementAllowed;
    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        movementAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementAllowed)
        {
            Movement();
        }

    }
    void Movement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
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
