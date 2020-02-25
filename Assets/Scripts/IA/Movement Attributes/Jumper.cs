using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a Rigidbody in order to apply force to the gameObject
[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// Jumps as high ad the "jump force" variable.
/// You can especify the delay between jumps, as well as the jump force. 
/// </summary>
public class Jumper : MonoBehaviour
{
    //Public attributes
    [SerializeField]
    private float jumpHeight = 1f;                            //How high you want this entity to jump


    /// <summary>
    /// Apply an impulse force to the GameObject to make it jump
    /// </summary>
    public void Jump()
    {
        float jumpForce = CalculateJumpSpeed();
        GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpForce;
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
    /// Calculate the jump initial speed that will be applied to the entity
    /// It follow the formula: √(2*g*Y) 
    /// being Y the max height, provided in Unity units
    /// </summary>
    /// <returns>A float with the initial speed</returns>
    private float CalculateJumpSpeed()
    {
        Debug.Log(Physics2D.gravity.magnitude);
        return Mathf.Sqrt(2 * jumpHeight * Physics2D.gravity.magnitude);
    }
}
