using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controls;
    public float speed = 12f;
    public float jumpvel = 4f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;

    Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -4f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = 4f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controls.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controls.Move(velocity * Time.deltaTime);
        
    }
}
