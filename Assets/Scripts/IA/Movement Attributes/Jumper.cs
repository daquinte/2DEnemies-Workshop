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
    private float jumpHeight = 8f;                            //How high you want this entity to jump

    [SerializeField]
    private float jumpTime = 1.5f;                              //Time that will take to reach max jump

    protected bool isForwardJumper = false;
    private Animator jumpAnimator;


    private void Start()
    {
        jumpAnimator = this.GetComponent<Animator>();
        GetComponent<Rigidbody2D>().gravityScale = 1;       //while this might seem redundant, we need this component to be affected by physics

        if (!isForwardJumper) { 
        StartCoroutine("JumpAfterDelay");
        }
    }

    /// <summary>
    /// Apply an impulse force to the GameObject to make it jump
    /// </summary>
    public void Jump()
    {
        float jumpForce = CalculateJumpSpeed();
        GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpForce;
        //jumpAnimator.SetBool("Jumping", true);
    }

    public void StopJumpAnimation()
    {
        jumpAnimator.SetBool("Jumping", false);
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
    /// Makes this jumper component as a part of a forward jumper.
    /// </summary>
    public void SetAsForwardJumper()
    {
        isForwardJumper = true;
    }

    /// <summary>
    /// Calculate the jump initial speed that will be applied to the entity
    /// It follow the formula: √(2*g*Y) 
    /// being Y the max height, provided in Unity units
    /// </summary>
    /// <returns>A float with the initial speed</returns>
    private float CalculateJumpSpeed()
    {
        //return Mathf.Sqrt(2 * jumpHeight * Physics2D.gravity.magnitude);
        return ((2 * jumpHeight) / jumpTime);
    }


    private IEnumerator JumpAfterDelay()
    {
        yield return new WaitForSeconds(1);
        Jump();
        yield return null;
    }
}
