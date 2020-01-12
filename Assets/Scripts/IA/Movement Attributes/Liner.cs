using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy moves in a straight line directly to point of the screen.
/// If rotateTowardsTarget is active, this component changes the direction of the gameObject to orientate itself.
/// Usage: A enemy you would want to move from one spot to another, either randomly or between fixed points.
/// </summary>
public class Liner : MovementBehaviour
{
    public float    movementSpeed = 0.40f;                 //Speed at which the entity moves in Unity units per second
    public float    rotationSpeed = 0.60f;                 //Speed at which the entity rotates in Unity units per second?
    public bool     rotateTowardsTarget;                   //whether you want the entity to rotate or not

    public GameObject    target;
    public Vector3      targetPoint;


    // Start is called before the first frame update
    void Start()
    {
        if(target == null) target = GameObject.FindGameObjectWithTag("Player");

        if (rotateTowardsTarget)
        {
            RotateToTarget();

        }
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = target.transform.position;
        //Operacion que necesito:  transform.position = Vector3.Lerp(transform.position, targetPoint, movementSpeed * Time.deltaTime); 

        if (targetPoint != transform.position) {
            //move towards the target

            float step = movementSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);


        }

  
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPoint = new Vector3(x, y);
       
    }

    private void RotateToTarget()
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object

        if (target.transform.position.x > transform.position.x)
        {
            transform.rotation = (Quaternion.Euler(0, 180, -AngleDeg));
        }
        else transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 180);
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

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }
}
