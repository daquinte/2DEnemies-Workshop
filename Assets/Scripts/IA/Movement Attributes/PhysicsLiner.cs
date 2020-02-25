using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsLiner : Liner
{
    [Tooltip("Speed at which the entity will move in Unity units/second")]
    public float speed = 1;

    [Tooltip("How long it will take for the enemy to reach the end point")]
    public float time = 2F;

    public override Vector2 GetMovement()
    {
        if (transform.position != targetPoint)
        {
            Vector2 steering = Vector2.zero;

            Vector3 desiredVelocity = targetPoint - transform.position;
            desiredVelocity.Normalize();
            desiredVelocity *= 5;

            //A la velocidad que llevase, tengo que aplicarle la mía
            steering = desiredVelocity - enemyEngine.GetVelocity();

            // And then smoothing it out and applying it to the character
            //steering = Vector3.SmoothDamp(steering, desiredVelocity, ref velocity, smoothTime);
            t += Time.deltaTime / time;
            steering = Vector3.Lerp(steering, desiredVelocity, t);
            lastMovement = steering;
        }
        else
        {
            lastMovement = Vector3.zero;
        }
        return lastMovement;
    }
}

//TODO: TEST