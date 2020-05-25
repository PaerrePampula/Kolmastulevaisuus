using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("PlayerSpeed", playerSpeed);
    }
}
