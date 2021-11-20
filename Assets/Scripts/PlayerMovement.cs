using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controls;
    public Transform body;
    public Transform camera;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 12f;
    public float slidevel = 16f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    bool isGrounded;
    bool slide = false;

    Vector3 velocity;
    float slide_speed;
    float current_speed;
    float heightinit;
    Vector3 camerainit;
    void Start()
    {
        heightinit = controls.height;
        camerainit = camera.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = 5.4f;
        }

        velocity.y += gravity * Time.deltaTime;
        controls.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controls.Move(move * speed * Time.deltaTime);

        slide_speed = Mathf.Sqrt(Mathf.Pow(12f,2f) + Mathf.Pow(0.12f,2f));
        current_speed = Mathf.Sqrt(Mathf.Pow(controls.velocity.x, 2f) + Mathf.Pow(controls.velocity.z, 2f));

        if (Input.GetKeyDown(KeyCode.LeftControl) && current_speed > slide_speed / 1.5)
        {
            slide = true;
        }
        if(slide)
        {
            controls.height = heightinit / 4;
            camera.localPosition = new Vector3(camerainit.x, camerainit.y / 3, camerainit.z);
            controls.Move(move * slidevel * Time.deltaTime);
            slidevel -= 0.05f;
            if (slidevel <= -11f)
            {
                slide = false;
                controls.height = heightinit;
                slidevel = 20f;
            }
        }
    }
}
