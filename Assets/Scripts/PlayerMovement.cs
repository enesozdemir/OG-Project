using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isDoubleJumpPossible;
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


        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isDoubleJumpPossible = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
        }
        else if (Input.GetButtonDown("Jump") && isDoubleJumpPossible)
        {
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
