using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy moves in a straight line directly to point of the screen.
/// If rotateTowardsTarget is active, this component changes the direction of the gameObject to orientate itself.
/// Usage: A enemy you would want to move from one spot to another, either randomly or between fixed points.
/// </summary>
public class Liner : MonoBehaviour
{
    public float    movementSpeed = 0.40f;                 //Speed at which the entity moves in Unity units per second
    public float    rotationSpeed = 0.60f;                 //Speed at which the entity rotates in Unity units per second?
    public bool     rotateTowardsTarget;                   //whether you want the entity to rotate or not

    public Vector3 targetPoint;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPoint != transform.position) { 
            //move towards the target
            transform.position = Vector3.Lerp(transform.position, targetPoint, movementSpeed * Time.deltaTime);
        }

        if (rotateTowardsTarget)
        {
            rotateToTarget();
            
        }
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPoint = new Vector3(x, y);

       
    }

    private void rotateToTarget()
    {
        Vector3 relativePos = transform.position - targetPoint;
        Quaternion rotation = Quaternion.LookRotation(relativePos); //NO, USAR ROTACIONES DE TRANSFORM
        rotation.x = transform.rotation.x;
        rotation.y = transform.rotation.y;
        transform.rotation = rotation;
    }

    /// <summary>
    /// Gizmos for the editor.
    /// Draw a yellow sphere at the targets's position
    /// Draw a blue line to check the route
    /// </summary>
    private void OnDrawGizmosSelected()
    {
       
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPoint, .3f);
       
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPoint);
    }
}
