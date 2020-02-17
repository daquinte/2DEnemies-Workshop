using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires...
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Bullet))]

/// <summary>
/// This component combines a jumper behaviour with a bullet behaviour.
/// It is needed because we do not want to jump forward all the time, instead this enemy will jump
/// according to the timers. 
/// </summary>
public class ForwardJumper : MovementBehaviour
{
    //Public attributes
    public float delayBetweenJumps;                     //Delay the entity stays on the ground before jumping
    //Private attributes

    [SerializeField] private Jumper jumper;             //This object´s Jumper component
    [SerializeField] private Bullet bullet;             //This object´s Bullet component
    private GameObject groundPoint;                     //A position marking where to check if the player is grounded. Created dinamically.

    
    private bool canJump;                               //¿Are you touching the ground, and your delay is over?
    private float lastJumpTimer;                        //Tracks the last frame in which you jumped
    private float groundCheckRadius = 0.2f;             //Radius of the sphere we use to track the ground.

    LayerMask groundMask;                               //Ground layer



    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        canJump = true;
        lastJumpTimer = Time.deltaTime;
        groundMask = (LayerMask.GetMask("Ground"));
        SetUpGroundPoint();
    }

    public override Vector2 GetMovement()
    {
        Vector2 steering = Vector2.zero;
        if (canJump)
        {
            jumper.Jump();                              //Jump!         
            steering += bullet.GetBulletMovement();     
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

        return steering;
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

    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    private void OnDrawGizmosSelected()
    {

        Renderer rend;


        rend = GetComponent<Renderer>();

        // A sphere that fully encloses the bounding box.
        Vector3 center = rend.bounds.center;
        float radius = rend.bounds.extents.magnitude;


        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center, radius);
    }

    
}
