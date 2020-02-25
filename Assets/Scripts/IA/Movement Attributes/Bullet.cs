using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves in a straight line in a given direction: left or right
/// Contrary to the Enemy Bullet, this is just a define of the lateral movement, but its not a bullet itself
/// </summary>
public class Bullet : MovementBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.5f;


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
