﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy moves in a straight line directly to point of the screen.
/// If rotateTowardsTarget is active, this component changes the direction of the gameObject to orientate itself.
/// Usage: A enemy you would want to move from one spot to another, either randomly or between fixed points.
/// </summary>
public class Liner : MovementBehaviour
{
    public float movementSpeed = 0.40f;                 //Speed at which the entity moves in Unity units per second
    public float rotationSpeed = 0.60f;                 //Speed at which the entity rotates in Unity units per second?
    public bool rotateTowardsTarget;                   //whether you want the entity to rotate or not
    public float smoothTime = 0.3F;                     //Movement smoothing
    
    private Vector3 targetPoint;                        //The end point of the line
    private Vector3 velocity = Vector3.zero;            //Velocity for the smoothdamp
    // Start is called before the first frame update
    new void Start()
    {

        base.Start();
        targetPoint = enemyEngine.GetTargetPosition();
        if (rotateTowardsTarget)
        {
            RotateToTarget();
        }
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPoint = new Vector3(x, y);
    }

    /// <summary>
    /// This method is called from the enemy engine and it does hell of a lot of things!!!!
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetMovement()
    {
        if (transform.position != targetPoint)
        {
            Vector2 steering = Vector2.zero;

            Vector3 desiredVelocity = targetPoint - transform.position;
            desiredVelocity.Normalize();
            desiredVelocity *= 5.0f;

            //A la velocidad que llevase, tengo que aplicarle la mía
            steering = desiredVelocity - enemyEngine.GetVelocity();
          
            // And then smoothing it out and applying it to the character
            steering = Vector3.SmoothDamp(steering, desiredVelocity, ref velocity, smoothTime);

            lastMovement = steering;
        }
        else
        {
            lastMovement = Vector3.zero;
        }
        return lastMovement;
    }


    /// <summary>
    /// Gizmos for the editor.
    /// Draw a yellow sphere at the targets's position
    /// Draw a blue line to check the route
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(transform.position, enemyEngine.GetTargetPosition());
    }



    private void RotateToTarget()
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(targetPoint.y - transform.position.y, targetPoint.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object

        if (enemyEngine.GetTargetPosition().x > transform.position.x)
        {
            transform.rotation = (Quaternion.Euler(0, 180, -AngleDeg));
        }
        else transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 180);
    }

}