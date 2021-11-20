using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controls;
    public Transform body;
    public Transform camera;
    public float speed = 12f;
    public float slidevel = 16f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
<<<<<<< Updated upstream
    bool isGrounded;
    bool slide = false;
=======
    
    public bool isGrounded, slide = false;
    public bool button_release = false;

    Vector3 velocity, camerainit, wall_jump_propulsion;

    public GameObject current_wall;
    public GameObject prev_wall;

    bool wall_jump = false;

    private float slide_speed, heightinit, current_speed;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
        StayOnGround() ;

        velocity.y += gravity * Time.deltaTime;
        controls.Move(velocity * Time.deltaTime);

        controls.Move(move() * speed * Time.deltaTime);

        slide_speed = Mathf.Sqrt(Mathf.Pow(12f,2f) + Mathf.Pow(0.12f,2f));
        current_speed = Mathf.Sqrt(Mathf.Pow(controls.velocity.x, 2f) + Mathf.Pow(controls.velocity.z, 2f));

        isSliding(move(), slide_speed, current_speed);
    }

    //Yeri denetler 
    //Oyuncunun yerde kalmasını ve zıplamasını sağlar
    void StayOnGround()
    {
>>>>>>> Stashed changes
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -4f;
            velocity.x = 0;
            velocity.z = 0;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = 5.4f;
        }

<<<<<<< Updated upstream
        velocity.y += gravity * Time.deltaTime;
        controls.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controls.Move(move * speed * Time.deltaTime);

        slide_speed = Mathf.Sqrt(Mathf.Pow(12f,2f) + Mathf.Pow(0.12f,2f));
        current_speed = Mathf.Sqrt(Mathf.Pow(controls.velocity.x, 2f) + Mathf.Pow(controls.velocity.z, 2f));

        if (Input.GetKeyDown(KeyCode.LeftControl) && current_speed > slide_speed / 1.5)
=======
    //Oyuncunun kaymasını sağlar
    void isSliding(Vector3 move, float slide_speed, float current_speed)
    {
        if(!button_release)
            slide = Input.GetKey(KeyCode.LeftControl) && current_speed > slide_speed / 1.5;
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StartCoroutine(CooldownTimer());
        }
        if (slide)
>>>>>>> Stashed changes
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
                button_release = true;
                controls.height = heightinit;
                slidevel = 16f;
                StartCoroutine(CooldownTimer());
            }
        }
    }
<<<<<<< Updated upstream
=======

    private Vector3 move()
    {
        return transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
    }

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(0.8f);
        controls.height = heightinit;
        button_release = false;
        slidevel = 16f;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // wall_jump_propulsion = hit.normal * Vector3.Dot(move(), hit.normal);
        current_wall = hit.gameObject;
        if (prev_wall != current_wall)
        {
            velocity.x = 0;
            velocity.z = 0;
            wall_jump = true;
            if (current_wall.layer == 7)
            {
                if ((Input.GetButtonDown("Jump") && !isGrounded) && wall_jump == true)
                {
                    velocity.y = 6.3f;
                    wall_jump = false;
                    prev_wall = hit.gameObject;
                }
            }
        }
        if (wall_jump == false)
        {
            velocity.x = hit.normal.x * 12;
            velocity.z = hit.normal.z * 12;
        }
    }
>>>>>>> Stashed changes
}
