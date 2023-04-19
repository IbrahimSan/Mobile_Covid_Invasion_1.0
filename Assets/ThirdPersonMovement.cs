using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Third Person Movement
    public CharacterController controller;

    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Calling Third Person Movement
        PCMovement();

        //Movement Animation

        // Code Key for Directions
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    // PC Movement
    void PCMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        
        //else
        //{
        //    anim.SetBool("running", false);
        //}

        //if (horizontal != 0f || vertical != 0f)
        //{
        //    //PlayerController RemoveFocus()
        //    anim.SetBool("running", true);
        //}
        //else
        //{
        //    anim.SetBool("running", false);

        //}

    }

}
