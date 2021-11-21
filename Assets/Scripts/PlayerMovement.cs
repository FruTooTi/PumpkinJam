using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controls;
    public float speed = 12f;
    public float jumpvel = 4f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public bool movementEnabled = true;
    public float forwardDashForce;
    public float dashDuration;
    public Hook hook;
    public Transform camera;
    bool isGrounded;
    public bool isFailed;

    Vector3 velocity;

    public static PlayerMovement Instance;

    private bool isDashing;
    IEnumerator DashIEnumerator()
    {
        Vector3 velocityAmount = camera.forward * forwardDashForce;
        velocity += velocityAmount;
        yield return new WaitForSeconds(dashDuration);
        velocity -= velocityAmount;
        isDashing = false;
    }

    public void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(DashIEnumerator());
            isDashing = true;
        }
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -4f;
        }

        float x = 0;
        float z = 0;
        if (movementEnabled)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = 4f;
            }

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");


            Vector3 move = transform.right * x + transform.forward * z;
            controls.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controls.Move(velocity * Time.deltaTime);
            
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                hook.StretchHook();
            }
        }

        if (!isFailed && transform.position.y < -30f)
        {
            GameManager.Instance.GameOver(GameManager.GameOverStatus.FellOff);
            isFailed = true;
        }
    }
}
