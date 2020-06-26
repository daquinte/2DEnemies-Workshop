using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Floater can float, fly or levitate. 
/// An entity with this behaviour will oscilate a given distance.
/// </summary>
/// 

[RequireComponent(typeof(Rigidbody2D))]

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Floater")]
public class Floater : MovementBehaviour
{
    public enum MovementAxis { X, Y };                                  //Axis in which a Floater can move

    [Tooltip("Wave amplitude of this movement")]
    public float movementAmplitude = 4f;                                //Total distance you want to cover in unity units

    [Tooltip("Time to reach the target Point")]
    public float timeToCompleteMovement = 1f;

    [Tooltip("Amount of time that the enemy will wait at the edges")]
    public float delayTime = 0.4f;                                      //Delay in Realtime Seconds the entity will wait at the ends of each path

    [Tooltip("Axis the enemy will float on")]
    public MovementAxis movementAxis;


    //Private movement attributes
    private Vector2 UpPoint;                                            //Highest position of the movement
    private Vector2 DownPoint;                                          //Lowest position of the movement
    private Vector3 targetFloaterPosition;                              //Position you´re moving towards
    private Vector2 distanceToTarget;                                   //Distance to the target position

    private float upperLimit;                                           //private highest number of the movement
    private float lowerLimit;                                           //private lowest number of the movement

    private bool towardsUpperLimit;                                     //Are you going towards the upperLimit?
    private bool isFloaterMoving = true;                                //Are you moving right now, at all?
    private bool drawEditorGizmos = true;                               //Gizmos on?
    private bool hasABulletComponent = false;                           //Does this component have a bullet behaviour?

    private Rigidbody2D RB2D;                                           //Self Rigidbody


    // Use this for initialization
    void Start()
    {
        if (GetComponent<Bullet>() != null) hasABulletComponent = true;
        Setup();
        drawEditorGizmos = false;
        towardsUpperLimit = true;

    }
    void Setup()
    {
        if (movementAxis == MovementAxis.Y)
        {
            //We set the limits for Y
            upperLimit = transform.position.y + (movementAmplitude / 2f);
            lowerLimit = transform.position.y - (movementAmplitude / 2f);
            UpPoint = new Vector2(transform.position.x, upperLimit);
            DownPoint = new Vector2(transform.position.x, lowerLimit);
            //targetFloaterPosition = new Vector3(transform.position.x, upperLimit);
        }
        else if (movementAxis == MovementAxis.X)
        {
            //We set the limits for X
            upperLimit = transform.position.x + (movementAmplitude / 2f);
            lowerLimit = transform.position.x - (movementAmplitude / 2f);
            UpPoint = new Vector2(upperLimit, transform.position.y);
            DownPoint = new Vector2(lowerLimit, transform.position.y);
            //targetFloaterPosition = new Vector3(upperLimit, transform.position.y);
        }

        targetFloaterPosition = UpPoint;
        distanceToTarget = targetFloaterPosition - transform.position;
        RB2D = GetComponent<Rigidbody2D>();
        RB2D.gravityScale = 0;
    }
    private void FixedUpdate()
    {
        if (isFloaterMoving)
        {
            float dist = 0;

            //With 
            if (hasABulletComponent)
            {
                Vector2 velocidad = distanceToTarget / timeToCompleteMovement;
                switch (movementAxis)
                {
                    case MovementAxis.Y:
                        dist = Mathf.Abs(targetFloaterPosition.y - transform.position.y);
                        RB2D.velocity = new Vector2(RB2D.velocity.x, velocidad.y);
                        break;
                    case MovementAxis.X:
                        dist = Mathf.Abs(targetFloaterPosition.x - transform.position.x);
                        RB2D.velocity = new Vector2(velocidad.x, RB2D.velocity.y);
                        break;
                }
            }
            else
            {

                dist = Vector3.Distance(transform.position, targetFloaterPosition);
                RB2D.velocity = distanceToTarget / timeToCompleteMovement;
            }
            
            if (dist < 0.15f)
            {
                //STOP
                isFloaterMoving = false;
                RB2D.Sleep();
                towardsUpperLimit = !towardsUpperLimit;
                StartCoroutine(WaitForSeconds(delayTime));
            }

        }
    }

   

    private void OnWaitForSecondsEnd()
    {
        if (hasABulletComponent)
        {
            switch (movementAxis)
            {
                case MovementAxis.Y:
                    UpPoint = new Vector2(transform.position.x, upperLimit);
                    DownPoint = new Vector2(transform.position.x, lowerLimit);

                    break;
                case MovementAxis.X:
                    UpPoint = new Vector2(upperLimit, transform.position.y);
                    DownPoint = new Vector2(lowerLimit, transform.position.y);
                    break;
            }
        }
        if (towardsUpperLimit)
        {
             targetFloaterPosition = UpPoint;
        }
        else targetFloaterPosition = DownPoint;

        RB2D.WakeUp();
        distanceToTarget = targetFloaterPosition - transform.position;
        isFloaterMoving = true;
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnWaitForSecondsEnd();
        yield return null;
    }

    /// <summary>
    /// EDITOR ONLY - Gizmos for the Floater behaviour
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (drawEditorGizmos)
        {
            Vector3 gizmosPosition = transform.position; // (drawEditorGizmos) ? transform.position : positionOnStart;

            // Draws a line from this transform to the target
            Gizmos.color = Color.red;
            float halfDistance = movementAmplitude / 2;
            switch (movementAxis)
            {
                case MovementAxis.X:
                    Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x - halfDistance, gizmosPosition.y));
                    Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x + halfDistance, gizmosPosition.y));
                    break;
                case MovementAxis.Y:
                    Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x, gizmosPosition.y - halfDistance));
                    Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x, gizmosPosition.y + halfDistance));
                    break;
            }
        }
        else
        {
            Gizmos.color = Color.green;
            float distanceUp, distanceDown;
            switch (movementAxis)
            {
                case MovementAxis.X:
                    distanceUp = Mathf.Abs(upperLimit - transform.position.x);
                    distanceDown = Mathf.Abs(lowerLimit - transform.position.x);
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + distanceUp, transform.position.y));
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - distanceDown, transform.position.y));
                    break;
                case MovementAxis.Y:
                    distanceUp = Mathf.Abs(upperLimit - transform.position.y);
                    distanceDown = Mathf.Abs(lowerLimit - transform.position.y);
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + distanceUp));
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - distanceDown));
                    break;
            }

        }

    }

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }
}
