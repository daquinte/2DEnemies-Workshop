using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

/// <summary>
/// Moves in a straight line in a given direction: left or right
/// Contrary to the Enemy Bullet, this is just a define of the lateral movement, but its not a bullet itself
/// </summary>
public class Bullet : MovementBehaviour
{

    public bool collideWithWalls = true;

    [SerializeField]
    private bool cancelGravity = true;

    [SerializeField]
    private float bulletSpeed = 2.5f;


    public void Start()
    {
    
        GetComponent<Rigidbody2D>().velocity = -transform.right * bulletSpeed;
        
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
        cancelGravity = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("[Bullet] Player hit!");
        }
    }


    private void SetupBullet()
    {
        if (!collideWithWalls)
        {
            //Si el enemigo tiene esto activado su boxcollider pasará a ser TRIGGER y comprobará colisiones por trigger en lugar de por bounding box!!!
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (cancelGravity)
        { 
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
