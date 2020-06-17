using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/ForwardJumper")]

/// <summary>
/// This component combines a jumper behaviour with a bullet behaviour.
/// It is needed because we do not want to jump forward all the time, instead this enemy will jump
/// according to the timers. 
/// </summary>
public class ForwardJumper : MovementBehaviour
{
    //Public attributes
    [Tooltip("Lateral speed movement. 0 = Jumper")] //TODO: make a log warning if == 0? could be nice
    public float movementSpeed = 1.5f;

    [Tooltip("Max Height to be reached")]
    public float jumpHeight = 4f;

    [Tooltip("Delay the entity stays on the ground before jumping")]
    public float jumpDelay = 1.5f;

    [Tooltip("Time that this component will wait before sending player position to Jumper component")]
    public float timeToRefresh = 1f;


    //Private attributes 
    private Jumper jumper;                              //This object´s Jumper component
    private Bullet bullet;                              //This object´s Bullet component
    private GameObject groundPoint;                     //A position marking where to check if the enemy is grounded. Created dinamically.
    private Animator jumpAnimator;                      //This object´s animator
    private Vector2 GizmosPos;                    

    private bool canJump;                               //Are you touching the ground, and your delay is over?
    private bool updatePlayerPosition = true;           //Can you update player position?
    private bool drawEditorGizmos = true;               //Are you moving right now, at all?

    private int jumperModifier;                         //Modifies the direction of the jump
    private float lastJumpTimer;                        //Tracks the last frame in which you jumped
    private float groundCheckRadius = 0.5f;             //Radius of the sphere we use to track the ground.



    // Start is called before the first frame update
    void Start()
    {
        GizmosPos = new Vector2(transform.position.x, transform.position.y + jumpHeight);   //Highest point
        jumpAnimator = GetComponent<Animator>();
        jumpAnimator.SetBool("Alert", true);
        
        canJump = true;
        drawEditorGizmos = false;
        lastJumpTimer = Time.deltaTime;
        
        SetUpComponents();
        
    }

    private void OnEnable()
    {
        //jumpAnimator.SetBool("Alert", true);
    }

    private void OnDisable()
    {
        //jumpAnimator.SetBool("Alert", false);
        //jumpAnimator.SetBool("Jumping", false);
    }


    private void Update()
    {
        if (canJump)
        {
            
            jumper.Jump();  //Jump!     

            if (updatePlayerPosition)
            {
                //Where do we jump now to get to the target?    
                jumperModifier = (transform.position.x > enemyEngine.GetTargetPosition().x) ? 1 : -1;
                RotateToTarget();
                updatePlayerPosition = false;
                StartCoroutine(SetTargetPositionAfterSeconds());
            }

            GetComponent<Rigidbody2D>().velocity += bullet.GetMovement() * jumperModifier;    //Move towards target
            canJump = false;                                //We shall not jump until next timer states so
        }
        else
        {
            if (Time.time - lastJumpTimer > jumpDelay)
            {
                //Cast a 2-width box into the ground. The box is so that the entity doesn´t get stuck in platform´s edges
                //((which used to happen and was pretty annoying))

                List<RaycastHit2D> groundRay = new List<RaycastHit2D>();
                ContactFilter2D contactFilter2D = new ContactFilter2D();
                int groundRayCount = Physics2D.BoxCast(groundPoint.transform.position, new Vector2(2, groundCheckRadius), 0f, Vector2.down, contactFilter2D, groundRay, groundCheckRadius);

                if (groundRayCount != 0)
                {
                    int i = 0;
                    bool stop = false;

                    while (i < groundRayCount && !stop)
                    {
                        if(groundRay[i].collider.gameObject.layer == GameManager.instance.GetGroundLayer())
                        {
                            canJump = true;
                            lastJumpTimer = Time.time;
                            stop = true;
                        }
                        i++;
                    }
                }

            }
        }
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
        jumper.SetAsForwardJumperComponent(out groundPoint);

        //Bullet
        bullet = gameObject.AddComponent(typeof(Bullet)) as Bullet;
        bullet.SetBulletSpeed(movementSpeed);
        bullet.SetAsGravityBullet();
        bullet.SetAsForwardJumperComponent();

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    
    private void OnDrawGizmosSelected()
    {

        if (drawEditorGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + jumpHeight));
        }
        else
        {
            Gizmos.color = Color.red;
            GizmosPos = new Vector2(transform.position.x, transform.position.y + jumpHeight);   //Highest point, updated
            float distance = Vector2.Distance(transform.position, GizmosPos);
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + distance));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == GameManager.instance.GetGroundLayer())
        {
            jumper.StopJumpAnimation();
        }
    }

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator SetTargetPositionAfterSeconds()
    {
        yield return new WaitForSecondsRealtime(timeToRefresh);
        updatePlayerPosition = true;
    }
}
