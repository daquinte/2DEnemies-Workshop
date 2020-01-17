﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a Rigidbody in order to apply force to the gameObject
[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// Jumps as high ad the "jump force" variable.
/// You can especify the delay between jumps, as well as the jump force. 
/// 
/// USAGE:
/// Combined with a Liner, a Jumper enemy will bounce towards another gameObject, usually the Player.
/// </summary>
public class Jumper : MonoBehaviour
{
    
    //Public attributes
    public float delayBetweenJumps;                     //Delay the entity stays on the ground before jumping
    public float jumpHeight;                            //How high you want this entity to jump

    //Private attributes
    private GameObject  groundPoint;                    //A position marking where to check if the player is grounded. Created dinamically.
    private Rigidbody2D RB2D;                           //This object´s rigidbody

    private bool        canJump;                        //¿Are you touching the ground, and your delay is over?
    private float       lastJumpTimer;                  //Tracks the last frame in which you jumped
    private float       groundCheckRadius = 0.2f;       //Radius of the sphere we use to track the ground.

    LayerMask groundMask;                               //Ground layer


    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        lastJumpTimer = Time.deltaTime;
        RB2D = GetComponent<Rigidbody2D>();
        groundMask = (LayerMask.GetMask("Ground"));
        SetUpGroundPoint();
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


    // Update is called once per frame
    void Update()
    {
        if (canJump)
        {
            Jump();
            canJump = false;
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
    /// Apply an impulse force to the GameObject to make it jump
    /// </summary>
    private void Jump()
    {
        RB2D.AddForce(new Vector2(0, jumpHeight * 1.5f));
        
        //TODO: Rotar
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
