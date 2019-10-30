using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Jumper enemy will bounce towards another entity. 
/// TODO: Redactar los atributos, una vez los tenga claros
/// </summary>
public class Jumper : MonoBehaviour
{
    //TODO: COMENTAR ATRIBUTOS
    //Public attributes
    public float delayBetweenJumps;
    public float jumpForce;

    //Private attributes
    private GameObject groundPoint;                  //A position marking where to check if the player is grounded. Created dinamically.
    private Rigidbody2D RB2D;
    
    private bool canJump;
    private float lastJumpTimer;
    private float groundCheckRadius = 0.2f;

    LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        lastJumpTimer = Time.deltaTime;
        RB2D = GetComponent<Rigidbody2D>();
        groundMask = (LayerMask.GetMask("Ground"));
        SetUpGroundPoint();
    }

    private void SetUpGroundPoint()
    {
        groundPoint = new GameObject("JumperGroundPoint");
        Renderer rend = GetComponent<Renderer>();
        groundPoint.transform.position = new Vector2(transform.position.x, rend.bounds.min.y);
        groundPoint.transform.parent = gameObject.transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (canJump)
        {

            RB2D.AddForce(new Vector2(-jumpForce, jumpForce*1.5f), ForceMode2D.Impulse);
            canJump = false;
        }
        else
        {
            if ((Time.time - lastJumpTimer > delayBetweenJumps))
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

    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    void OnDrawGizmosSelected()
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
