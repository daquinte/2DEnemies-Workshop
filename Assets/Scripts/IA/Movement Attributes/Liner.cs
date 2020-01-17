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

    //PRUEBA
    private Vector3 velocity;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        
        base.Start();
        if (rotateTowardsTarget)
        {
          RotateToTarget();
        }
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPoint = new Vector3(x, y);
       
    }

    private void RotateToTarget()
    {
        Vector3 targetPos = enemyEngine.GetTargetPosition();
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object

        if (targetPos.x > transform.position.x)
        {
            transform.rotation = (Quaternion.Euler(0, 180, -AngleDeg));
        }
        else transform.rotation = Quaternion.Euler(0, 0, AngleDeg-180);
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
        Debug.Log("Seek");
        Vector2 steering = Vector2.zero;

        Vector3 desiredVelocity = enemyEngine.GetTargetPosition() - transform.position;
        desiredVelocity.Normalize();
        desiredVelocity *= 5.0f;

        //A la velocidad que llevase, tengo que aplicarle la mía
        steering = desiredVelocity - enemyEngine.GetVelocity();

        lastMovement = steering;
        return lastMovement;
    }
}
