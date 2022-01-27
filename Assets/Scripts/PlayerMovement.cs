using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 12f;
    public float runSpeed = 18f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isDoubleJumpPossible;
    public bool isSprinting = false;
    public bool isCrouching = false;
    public float crouchingHeight = 1.5f;
    public float standingHeight = 2f;
    public float sprintingMultiplier;
    public float jumpHeight = 2f;
    public Vector3 velocity;
    public Camera fpsCam;
    public Interactable focus;


    // Update is called once per frame
    void Update()
    {
        //Checking if player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //Forcing to stay on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Character Movement and Rotation
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * walkSpeed * Time.deltaTime);

        //Runing
        if (Input.GetKeyDown(KeyCode.LeftShift) && Stamina.instance.stamina > 1f)
        {                       
            isSprinting = true;            
            walkSpeed = runSpeed;            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Stamina.instance.stamina < 1f)
        {
            walkSpeed = 12f;
            isSprinting = false;
        }

        //Stamina Consumption for running rule
        if (isSprinting && Stamina.instance.stamina > 1f)
        {
            Stamina.instance.stamina -= 10f * Time.deltaTime;
        }

        //Crouching
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            isGrounded = true;

            if (isCrouching)
            {
                isCrouching = true;
                controller.height = crouchingHeight;
                controller.center = new Vector3(0f, 0.5f, 0f);
                //groundCheck transform position must be checked, we cannot jump while we crouch!
            }
            else
            {                
                isCrouching = false;
                controller.height = standingHeight;
                controller.center = new Vector3(0f, 0f, 0f);
            }            
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded && Stamina.instance.stamina > 10f)
        {
            Stamina.instance.stamina -= 200f * Time.deltaTime;
            isDoubleJumpPossible = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
        }
        else if (Input.GetButtonDown("Jump") && isDoubleJumpPossible && Stamina.instance.stamina > 10f)
        {
            Stamina.instance.stamina -= 150f * Time.deltaTime;
            velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
            isDoubleJumpPossible = false;
        }
        
        //Gravity Implication
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Aim and interact/pickup
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        if (controller.velocity.magnitude > 0)
        {
            RemoveFocus();
        }

    }

    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            
            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        
        focus = null;        
    }
}
