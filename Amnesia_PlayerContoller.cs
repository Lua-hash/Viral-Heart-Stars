using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Amnesia_PlayerContoller : MonoBehaviour
{
    #region Variable Fields
    //This scripts Public Fields
    public float Movespeed = 2;
    public float RunSpeedModifier = 2f;
    public Transform groundCheckCollider;
    public LayerMask GroundLayer;
    public bool isGrounded = false;
    public float JumpForce = 300;

    //This scripts private fields
    private Rigidbody2D Rb;
    private Animator animator;

    private const float GroundCheckRadius = 0.2f;
    private float Hmovement;
    private bool Jump = false;
    private bool isRunning = false;
    private bool Faceright = true;
    #endregion


    #region Void Awake
    // void Awake() is called the moment the script is loaded within the game.
    void Awake()
    {
        //Setting the RigidBody up
        Rb = GetComponent<Rigidbody2D>();

        //Setting the Animator up
        animator = GetComponent<Animator>();
    }
    #endregion


    #region Void Update
    // void Update() is called once per frame
    void Update()
    {
        // getting the movement input of the player
        // Input.GetAxis would get the range of inputs(how far they press on the direction). But, since we are not using controllers, Input.GetAxisRaw works better.
        // You can go into Edit>Project Settings>Input Manager to change the "Horizontal" and "Vertical" input keys
        Hmovement = Input.GetAxisRaw("Horizontal");

        //If Left Shift is pressed, enable the player to run
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;

        //If Left Shift is released, stop the player from running
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
        //UnityEngine.Debug.Log(Hmovement);

        //If the player presses Jump, they are jumping
        if (Input.GetButtonDown("Jump"))
            Jump = true;
        //if they don't press jump then they are not jumping
        else if (Input.GetButtonUp("Jump"))
            Jump = false;
    }
    #endregion


    #region Ground Check
    void GroundCheck()
    {
        isGrounded = false;
        //Check if the GroundCheck object is colliding with the ground
        //If the GroundCheck object is colliding with the ground set isGrounded to true
        //If the GroundCheck object is colliding with the ground set isGrounded to true
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, GroundCheckRadius, GroundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
    }
    #endregion


    #region Fixed Update
    // void FixedUpdate() is called once every fixed number of frames (Typically used for physics interactions)
    //Always put code relating to the Rigidbody2D here 
    void FixedUpdate()
    {
        Move(Hmovement, Jump);
        GroundCheck();
    }
    #endregion


    #region Movement and Transforms
    // void Move(float dir) Takes in a direction to create and control player movement
    void Move(float dir, bool jumpFlag)
    {
        #region Horizontal Movement
        //Making the player move at a easily editable and consistant speed
        float xVal = dir * Movespeed * 1000 * Time.fixedDeltaTime;

        //If the player is running, multiply their speed by this
        if (isRunning)
            xVal *= RunSpeedModifier;

        //Create a Vector with two inputs for the velocity of the player
        Vector2 targetVelocity = new Vector2(xVal, Rb.velocity.y);

        //Set the players velocity
        Rb.velocity = targetVelocity;

        //Flipping the Character
        //Storing the current scale value of the Player as a Vector with three inputs
        Vector3 currentScale = transform.localScale;  

        //If the character is facing right and they move left, flip them so they are facing left
        if(Faceright && dir < 0)
        {
            transform.localScale = new Vector3(-100, 100, 1);
            Faceright = false;
        }

        //If the character is facing left and they move right, flip them so they are facing right
        else if(!Faceright && dir > 0)
        {
            transform.localScale = new Vector3(100, 100, 1);
            Faceright = true;
        }

        //UnityEngine.Debug.Log(Rb.velocity.x);
        //Set the xVelocity value in the animator equal to the character velocity
        animator.SetFloat("xVelocity", Mathf.Abs(Rb.velocity.x));
        #endregion


        #region Vertical Movement
        //If the player is on the ground and presses jump, make them Jump
        if(isGrounded && jumpFlag)
        {
            //Add JumpForce
            isGrounded = false;
            jumpFlag = false;
            Rb.AddForce(new Vector2(0f, JumpForce));
        }
        #endregion
    }
    #endregion
}

#region Notes
//Movespeeds below 2 are super sluggish
// Idle = 0   Walking = 40   Running = 80
#endregion