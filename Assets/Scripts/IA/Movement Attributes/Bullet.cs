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

    public bool collideWithWalls = true; //TODO: problema, si desactivo el boxcollider siempre no va a chocar con el jugador!!!

    [SerializeField]
    private float bulletSpeed = 0.5f;

    

    public void Start()
    {
        if (!collideWithWalls) 
                GetComponent<BoxCollider2D>().enabled = false;
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
}
