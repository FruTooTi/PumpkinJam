using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controls;
    public Transform body, groundCheck, camera ;
    public LayerMask groundMask;

    public float speed = 12f;
    public float slidevel = 14f;
    public float gravity = -9.85f;
    public float groundDistance = 0.4f;
    public float incresing_Speed = 1f;
    public float lastp, ChekingFall = 10f ;
    
    public bool isGrounded, slide = false;
    public bool button_release = false;

    Vector3 velocity, camerainit, wall_jump_propulsion;

    public GameObject current_wall;
    public GameObject prev_wall;

    bool wall_jump , LeftControlUpped;

    private float slide_speed, heightinit, current_speed;

    void Start()
    {
        heightinit = controls.height;
        camerainit = camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

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
    bool StayOnGround()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
            speed = 12f;
            current_wall = null;
            prev_wall = null;
            velocity.x = 0;
            velocity.z = 0;
        }
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += 11f;
        }

        if (velocity.x != 0)
        {
            if (velocity.x > 0)
                velocity.x -= 0.02f;
            else
                velocity.x += 0.02f;
        }
        if (velocity.z != 0)
        {
            if (velocity.z > 0)
                velocity.z -= 0.02f;
            else
                velocity.z += 0.02f;
        }
        return isGrounded ;
    }

    //Oyuncunun kaymasını sağlar
    void isSliding(Vector3 move, float slide_speed, float current_speed)
    {
        if(!slide)
            slide = Input.GetKeyDown(KeyCode.LeftControl) && current_speed > slide_speed / 1.5 && isGrounded;
        if (slide)
        {
            ChekingFall = transform.position.y - lastp ;
            lastp = transform.position.y ;
            controls.height = heightinit / 4;
            camera.localPosition = new Vector3(camerainit.x, camerainit.y / 3, camerainit.z);
            controls.SimpleMove(move * slidevel * Time.deltaTime);
            slidevel -= 0.05f ;
            if (slidevel <= -11f && ChekingFall >= 0 || Input.GetButtonDown("Jump"))
            {
                StartCoroutine(CooldownTimer()) ;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl) && !LeftControlUpped)
            {
                LeftControlUpped = true ;
            }
            if (slidevel < 2f && LeftControlUpped)
            {
                StartCoroutine(CooldownTimer()) ;
            }
            if (ChekingFall < 0)
            {
                controls.Move(move * Time.deltaTime * incresing_Speed) ;
                incresing_Speed += 0.04f ;
            }
            if (slidevel <= -11f && ChekingFall < 0)
            {
                slidevel = -12f;
            }
        }
    }

    private Vector3 move()
    {
        return transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
    }

    IEnumerator CooldownTimer()
    {
        slide = false;
        controls.height = heightinit;
        slidevel = 14f;
        LeftControlUpped = false ;
        incresing_Speed = 1f;
        yield return new WaitForSeconds(0.5f);
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
                    velocity.y = 5f;
                    wall_jump = false;
                    prev_wall = hit.gameObject;
                }
            }
        }
        if (wall_jump == false)
        {
            velocity.x = hit.normal.x * 15.5f;
            velocity.z = hit.normal.z * 15.5f;
            velocity.y = 10f ;
            speed = 6f;
            velocity.x = hit.normal.x * 15.5f;
            velocity.z = hit.normal.z * 15.5f;
            velocity.y = 10f ;
            wall_jump = true;
        }

        if (hit.gameObject.layer == 8)
        {
            velocity.y = 20f ;
        }
    }
}


