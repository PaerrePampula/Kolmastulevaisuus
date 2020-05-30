using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationController : MonoBehaviour
{
    #region Fields
    Animator animator;
    NavMeshAgent navMeshAgent;
    float playerSpeed;
    #endregion

    #region MonobehaviourDefaults
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
    #endregion
}
