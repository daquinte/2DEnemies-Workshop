using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Floater can float, fly or levitate. 
/// An entity with this behaviour will oscilate a given distance.
/// </summary>
/// 

[RequireComponent(typeof(Rigidbody2D))]

public class Floater : MovementBehaviour
{
    protected enum MovementAxis
    {
        X,
        Y
    };                   //Axis in which a Floater can move

    [Tooltip("Wave amplitude of this movement")]
    public float movementAmplitude = 4f;             //Total distance you want to cover in unity units

    [Tooltip("Movement Speed of this movement")]
    public float movementAcceleration = 1.5f;

    [Tooltip("Time to reach the target Point")]
    public float timeToCompleteMovement = 4;

    [Tooltip("Amount of time that the enemy will wait at the edges")]
    public float delayTime = 0.8f;                                      //Delay in Realtime Seconds the entity will wait at the ends of each path

    [SerializeField]
    private MovementAxis movementAxis = MovementAxis.Y;                 // Axis you want to float on

    //Private movement attributes

    private float upperLimit;                                           //"Higher" limit of the movement
    private float lowerLimit;                                           //"Lower" limit of the movement
    private float t;                                                    //temp t value for steering
        
    private bool towardsUpperLimit;                                     //Are you going towards the upperLimit?
    private bool isMoving = true;                                       //Are you moving right now, at all?
    private bool drawEditorGizmos = true;                                       //Are you moving right now, at all?
    private Vector3 targetFloaterPosition;                              //Position you´re moving towards
    private Vector3 positionOnStart;                                    //Position on start for Gizmos purposes

     private Rigidbody2D RB2D;                                           //Self Rigidbody


    // Use this for initialization
    void Start()
    {
        drawEditorGizmos = false;
        towardsUpperLimit = true;
        Setup();

    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            t += Time.deltaTime / timeToCompleteMovement;

            float dist;
            if (movementAxis == MovementAxis.Y)
            {
                dist = Mathf.Abs(targetFloaterPosition.y - transform.position.y);
                RB2D.velocity = new Vector2(RB2D.velocity.x, Vector2.Lerp(transform.position, targetFloaterPosition, t).y);

            }
            else
            {
                dist = Mathf.Abs(targetFloaterPosition.x - transform.position.x);
                RB2D.velocity = new Vector2(Vector2.Lerp(transform.position, targetFloaterPosition, t).x, RB2D.velocity.y);

            }

            if (dist < 0.25)
            {
                //STOP
                isMoving = false;
                RB2D.Sleep();
                towardsUpperLimit = !towardsUpperLimit;
                StartCoroutine(WaitForSeconds(delayTime));

            }
        }
    }

    void Setup()
    {
        positionOnStart = transform.position;
        if (movementAxis == MovementAxis.Y)
        {
            //We set the limits for Y
            upperLimit = transform.position.y + (movementAmplitude / 2f);
            lowerLimit = transform.position.y - (movementAmplitude / 2f);
            targetFloaterPosition = new Vector3(transform.position.x, upperLimit);
        }
        else if (movementAxis == MovementAxis.X)
        {
            //We set the limits for X
            upperLimit = transform.position.x + (movementAmplitude / 2f);
            lowerLimit = transform.position.x - (movementAmplitude / 2f);
            targetFloaterPosition = new Vector3(upperLimit, transform.position.y);
        }

        RB2D = GetComponent<Rigidbody2D>();
        RB2D.gravityScale = 0;
    }

    private void OnWaitForSecondsEnd()
    {
        float hey = towardsUpperLimit ? upperLimit : lowerLimit;

        if (movementAxis == MovementAxis.Y)
        {
            targetFloaterPosition = new Vector3(transform.position.x, hey);
        }
        else if (movementAxis == MovementAxis.X)
        {
            targetFloaterPosition = new Vector3(hey, transform.position.y);
        }
        RB2D.WakeUp();
        isMoving = true;
    }

    /// <summary>
    /// Called then the component is disabled.
    /// We stop all coroutines, either the X or Y axis corroutine will be stopped.
    /// </summary>
    private void OnDisable()
    {
        StopAllCoroutines();
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

        Vector3 gizmosPosition = (drawEditorGizmos) ? transform.position : positionOnStart;

        // Draws a line from this transform to the target
        Gizmos.color = Color.red;
        switch (movementAxis)
        {
            case MovementAxis.X:
                Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x - movementAmplitude / 2, gizmosPosition.y));
                Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x + movementAmplitude / 2, gizmosPosition.y));
                break;                                          
            case MovementAxis.Y:                                
                Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x, gizmosPosition.y - movementAmplitude / 2));
                Gizmos.DrawLine(transform.position, new Vector3(gizmosPosition.x, gizmosPosition.y + movementAmplitude / 2));
                break;
        }

    }

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }
}
