using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance =0.4f;
    public LayerMask groundMask;

    public bool hasKey = false;
    public bool isDead =false;
    public int syringeCollected = 0;
    bool isGrounded;
    
    Vector3 velocity;
    private Animator anim;
    public AudioSource soundDie;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    //private void Update()
    //{
        
    //}

    public void Move(float stamina)
    {
        // to check whether the player is on the floor
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }
        
        // to check whether player is in which state of moving
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        z = Mathf.Clamp01(z);
 
        Vector3 move = transform.right * x + transform.forward *z;
        //Vector3 move = transform.forward *z;
        if(isGrounded)
        {
            if (move!=Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();

            }
            else if (move!=Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                if (stamina<1)
                {
                    Walk();
                }
                else
                {
                    Run();
                }
            }
            else if (move==Vector3.zero)
            {
                Idle();
            }
        }
        controller.Move(move * speed * Time.deltaTime);

        velocity.y +=gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    public void Walk()
    {
        speed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    public void Run()
    {
        speed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    public void Death()
    {
        isDead = true;
        anim.SetBool("Death", isDead);
        soundDie.Play();
    }
    
}
