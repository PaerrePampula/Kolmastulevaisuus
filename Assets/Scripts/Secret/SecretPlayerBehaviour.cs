using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPlayerBehaviour : MonoBehaviour
{

    private float walkSpeed = 15f;
    private float runSpeed = 22f;
    private float jumpSpeed = 5f;
    private float gravity;

    private Vector3 moveDir;
    private Vector3 normalMovement; //Movement on z,x axis.

    private CharacterController characterController;

    private bool isSpeedBoosted;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        normalMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Standard movement on x,z axises
        normalMovement *= setSpeed(); //Give the player speed values to the inputs


        moveDir = new Vector3(normalMovement.x, gravity, normalMovement.z); //include gravity or jumping
        if (characterController.isGrounded) //if grounded, player can jump
        {
            if (Input.GetButton("Jump"))
            {
                gravity += jumpSpeed;
            }
        }
        if (!characterController.isGrounded) //if player is not grounded, increase y acceleration in the negative axis and increase that value by time as a constant.
        {
            gravity -= 20f * Time.deltaTime;
        }
        moveDir = transform.TransformDirection(moveDir);

        characterController.Move(moveDir * Time.deltaTime); //Move the player
    }
    float setSpeed() //aquire player speed according to keypress and pickups
    {
        float gottenSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            gottenSpeed = runSpeed;
        }
        else
        {
            gottenSpeed = walkSpeed;
        }
        if (isSpeedBoosted)
        {
            gottenSpeed *= 1.2f;
        }
        return gottenSpeed;
    }
    public void SpeedBoost()
    {
        isSpeedBoosted = true;
    }
    public void ApplyYForce(float force) //To be applied on triggers on jumppads etc.
    {
        gravity = 0; //Make the jump more consistent and remove being able to boost the jump too much
        gravity += force;
    }
}
