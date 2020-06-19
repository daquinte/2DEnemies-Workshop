using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Bullet")]

/// <summary>
/// Moves in a straight line in a given direction: Up, down, left or right
/// Contrary to the Enemy Bullet, this is just a define of the linear movement, but its not a bullet itself
/// </summary>
public class Bullet : MovementBehaviour
{
    private enum MovementDirection
    {
        Up, Down, Left, Right
    };

#if UNITY_EDITOR
    private void OnValidate()
    {
        BulletDirection = BulletDirection.normalized;
    }
#endif

    [Tooltip("Do you want collision with walls?")]
    public bool collideWithWalls = true;


    [Tooltip("Direction in which the bullet will move")]
    public Vector2 BulletDirection;
   // private MovementDirection movementDirection = MovementDirection.Up;

    [Tooltip("Movement in Unity units/second")]
    public float bulletSpeed = 1.5f;


    private Rigidbody2D RB2D;                       //This component´s rigid body
    private bool isForwardJumper = false;           //Is this component a sub-component of a Forward Jumper?
    private bool enableGravity = false;



    public void Start()
    {
        SetupBullet();
    }

    public void FixedUpdate()
    {
        if (!isForwardJumper)
        {
            /*switch (movementDirection)
            {
                case MovementDirection.Up:
                    RB2D.velocity = new Vector2(RB2D.velocity.x, Mathf.Abs(bulletSpeed));
                    break;
                case MovementDirection.Down:
                    RB2D.velocity = new Vector2(RB2D.velocity.x, -Mathf.Abs(bulletSpeed));
                    break;
                case MovementDirection.Left:
                    RB2D.velocity = new Vector2(-Mathf.Abs(bulletSpeed), RB2D.velocity.y);
                    break;
                case MovementDirection.Right:
                    RB2D.velocity = new Vector2(Mathf.Abs(bulletSpeed), RB2D.velocity.y);
                    break;
            }*/
            RB2D.velocity = new Vector2(BulletDirection.x * bulletSpeed, BulletDirection.y * bulletSpeed);

        }
    }

    /// <summary>
    /// Gets the movement of this Movement Behaviour
    /// </summary>
    /// <returns>Vector 2 of the next movement</returns>
    public override Vector2 GetMovement()
    {
        return Vector2.left * bulletSpeed;
    }

    public void SetBulletSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }

    //Makes the bullet behaviour be affected by gravity
    public void SetAsGravityBullet()
    {
        enableGravity = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("[Bullet] Player hit!");
        }
    }


    /// <summary>
    /// Sets this component as an essential part of a Forward Jumper
    /// Thus this component will not update itself but will provide the movement
    /// for the Forward Jumper behaviour
    /// </summary>
    public void SetAsForwardJumperComponent()
    {
        isForwardJumper = true;
    }

    /// <summary>
    /// Setups this component´s logitics. 
    /// Checks if it should collide will 
    /// </summary>
    private void SetupBullet()
    {

        if (!collideWithWalls)
        {
            //
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        RB2D = GetComponent<Rigidbody2D>();
        if (RB2D != null)
        {
            if (!enableGravity)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }


    }

    /// <summary>
    /// Gizmos for the Faller behaviour
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.red;
        Vector2 GizmosRayDirection = (Vector2)transform.position + BulletDirection * 3;
        Gizmos.DrawLine(transform.position, GizmosRayDirection);
    }
}
