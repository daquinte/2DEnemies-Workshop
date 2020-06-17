using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a Rigidbody in order to apply force to the gameObject
[RequireComponent(typeof(Rigidbody2D))]


//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Jumper")]

/// <summary>
/// Jumps as high ad the "jump force" variable.
/// You can especify the delay between jumps, as well as the jump force. 
/// </summary>
public class Jumper : MovementBehaviour
{
    //Public attributes
    [Tooltip("Delay between jumps")]
    [SerializeField]
    private float jumpDelay = 1.2f;                              //Delay between jumps

    [Tooltip("How high you want this entity to jump")]
    [SerializeField]
    private float jumpHeight = 4f;                              //How high you want this entity to jump


    private bool isForwardJumper = false;
    private bool canJump;                                       //¿Are you touching the ground, and your delay is over?
    private bool drawEditorGizmos = true;                       //Are you moving right now, at all?


    private float jumpTime = 1f;                                //Time that will take to reach max jump
    private float lastJumpTimer;                                //Tracks the last frame in which you jumped
    private float jumpForce = 0f;                               //Total force of this jump
    private float groundCheckRadius = 0.5f;                     //Radius of the sphere we use to track the ground.

    private Rigidbody2D RB2D;
    private GameObject groundPoint;
    private Animator jumpAnimator;

    private Vector2 GizmosPos;



    private void Start()
    {
        GizmosPos = new Vector2(transform.position.x, transform.position.y + jumpHeight);   //Highest point
        drawEditorGizmos = false;
        jumpAnimator = this.GetComponent<Animator>();
        RB2D = GetComponent<Rigidbody2D>();
        RB2D.gravityScale = 1;       //while this might seem redundant, we need this component to be affected by physics

        if (groundPoint == null)
        {
            SetUpGroundPoint();
        }
    }

    /// <summary>
    /// Sets the point which will track if you are on the ground or not.
    /// </summary>
    private void SetUpGroundPoint()
    {
        groundPoint = new GameObject("JumperGroundPoint");
        Renderer rend = GetComponent<Renderer>();
        groundPoint.transform.position = new Vector2(transform.position.x, rend.bounds.min.y - 0.1f);
        groundPoint.transform.parent = gameObject.transform;
    }

    private void Update()
    {
        if (!isForwardJumper)
        {
            if (canJump)
            {
                Jump();
                lastJumpTimer = Time.time;
                canJump = false;
            }
            else
            {
                if (Time.time - lastJumpTimer > jumpDelay)
                {
                    CheckIfGrounded();
                }
            }

        }
    }

    /// <summary>
    /// Apply an impulse force to the GameObject to make it jump
    /// </summary>
    public void Jump()
    {
        CalculateJumpSpeed();
        RB2D.velocity = new Vector2(RB2D.velocity.x, jumpForce);
        //jumpAnimator.SetBool("Jumping", true);
    }

    public void CheckIfGrounded()
    {
        //Cast a sphere of 0.2f radius from the groundPoint to check for ground
        RaycastHit2D groundRay = Physics2D.Raycast(groundPoint.transform.position, Vector2.down, groundCheckRadius);
        if (groundRay.collider != null)
        {
            if (groundRay.collider.gameObject.layer == GameManager.instance.GetGroundLayer())
            {
                canJump = true;
                lastJumpTimer = Time.time;

            }
        }
    }

    public void StopJumpAnimation()
    {
        //jumpAnimator.SetBool("Jumping", false);
    }

    /// <summary>
    /// Sets the JumpHeight
    /// </summary>
    /// <param name="jh">new Jumper Height</param>
    public void SetJumpHeight(float jh)
    {
        jumpHeight = jh;
    }

    /// <summary>
    /// Sets the JumpHeight
    /// </summary>
    /// <param name="jh">new Jumper Height</param>
    public void SetJumpDelay(float jd)
    {
        jumpDelay = jd;
    }

    /// <summary>
    /// Makes this jumper component as a part of a forward jumper.
    /// </summary>
    public void SetAsForwardJumperComponent(out GameObject gp)
    {
        isForwardJumper = true;
        if (groundPoint == null)
        {
            SetUpGroundPoint();
        }
        gp = groundPoint;
    }

    /// <summary>
    /// Calculate the jump initial speed that will be applied to the entity
    /// It follow the formula: √(2*g*Y) 
    /// being Y the max height, provided in Unity units
    /// </summary>
    /// <returns>A float with the initial speed</returns>
    private void CalculateJumpSpeed()
    {
        //return Mathf.Sqrt(2 * jumpHeight * Physics2D.gravity.magnitude);
        jumpForce = ((2 * jumpHeight) / jumpTime);
        RB2D.gravityScale = jumpHeight / 5;   //5 max heigh == 1 gravity, so we make the rule of three
    }

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmosSelected()
    {
        if (!isForwardJumper)
        {
            if (drawEditorGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + jumpHeight));
            }
            else
            {
                Gizmos.color = Color.red;
                float distance = Vector2.Distance(transform.position, GizmosPos);
                Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + distance));
            }
        }
    }
}
