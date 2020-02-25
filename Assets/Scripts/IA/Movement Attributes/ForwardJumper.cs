using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component combines a jumper behaviour with a bullet behaviour.
/// It is needed because we do not want to jump forward all the time, instead this enemy will jump
/// according to the timers. 
/// </summary>
public class ForwardJumper : MovementBehaviour
{
    //Public attributes
    [Tooltip("Lateral speed movement. 0 = Jumper")] //TODO: make a log warning if == 0? could be nice
    public float movementSpeed;

    [Tooltip("Max Height to be reached")]
    public float jumpHeight;

    [Tooltip("Delay the entity stays on the ground before jumping")]
    public float delayBetweenJumps;                    


    //Private attributes 
    private Jumper jumper;                              //This object´s Jumper component
    private Bullet bullet;                              //This object´s Bullet component
    private GameObject groundPoint;                     //A position marking where to check if the player is grounded. Created dinamically.
    private Rigidbody2D rb2d;                           //This object´s rigidbody 

    private bool canJump;                               //¿Are you touching the ground, and your delay is over?
    private float lastJumpTimer;                        //Tracks the last frame in which you jumped
    private float groundCheckRadius = 0.2f;             //Radius of the sphere we use to track the ground.

    LayerMask groundMask;                               //Ground layer



    // Start is called before the first frame update
    void Start()
    {
      
        rb2d = GetComponent<Rigidbody2D>();
        canJump = true;
        lastJumpTimer = Time.deltaTime;
        groundMask = (LayerMask.GetMask("Ground"));
        SetUpGroundPoint();
        SetUpComponents();
    }


    private void Update()
    {
        if (canJump)
        {
            jumper.Jump();                              //Jump!         
            rb2d.velocity += bullet.GetMovement();
            canJump = false;                            //We shall not jump until next timer states so
        }
        else
        {
            if (Time.time - lastJumpTimer > delayBetweenJumps)
            {
                //Cast a sphere of 0.2f radius from the groundPoint to check for ground
                RaycastHit2D groundRay = Physics2D.Raycast(groundPoint.transform.position, Vector2.down, groundCheckRadius, groundMask);
                if (groundRay.collider != null)
                {
                    canJump = true;
                }

                lastJumpTimer = Time.time;
            }
        }
    }

    /// <summary>
    /// Sets the point which will track if you are on the ground or not.
    /// </summary>
    private void SetUpGroundPoint()
    {
        groundPoint = new GameObject("JumperGroundPoint");
        Renderer rend = GetComponent<Renderer>();
        groundPoint.transform.position = new Vector2(transform.position.x, rend.bounds.min.y);
        groundPoint.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// Sets up the required components for this behaviour
    /// </summary>
    private void SetUpComponents()
    {
        //Jumper
        jumper = gameObject.AddComponent(typeof(Jumper)) as Jumper;
        Debug.Log("Jumper Height: " + jumpHeight);
        jumper.SetJumpHeight(jumpHeight);

        //Bullet
        bullet = gameObject.AddComponent(typeof(Bullet)) as Bullet;
        bullet.SetBulletSpeed(movementSpeed);
    }

    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    private void OnDrawGizmosSelected()
    {

        Renderer rend;


        rend = GetComponent<Renderer>();

        // A sphere that fully encloses the bounding box.
        Vector3 center = rend.bounds.center;
        float radius = rend.bounds.extents.magnitude;


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center, radius);
    }

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }
}
