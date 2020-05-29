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

    [Tooltip("Whether you want the entity to rotate or not")]
    public bool rotateTowardsTarget = true;

    [Tooltip("Time to reach the target Point")]
    public float timeToReachTarget = 2;

    [Tooltip("Decides the Liner type")]
    [SerializeField]
    protected LinerType linerType = LinerType.Constant;

    [Tooltip("Acceleration")]
    public float Acceleration = 1f;

    protected enum LinerType { Acelerated, Constant };

    protected float t;                                    //temp t value for steering
    protected Vector3 targetPoint;                        //The end point of the line

    //Cinematic attributes
    private Vector3 startPosition;

    //Acelerated attributes
    private Rigidbody2D RB2D;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = enemyEngine.GetTargetPosition();
        SetupLiner();
    }


    private void SetupLiner()
    {
        //This makes sure that whenever this two components interact, they do it correctly
        if (GetComponent<Bullet>() != null && GetComponent<FollowerLiner>() == null)
        {
            linerType = LinerType.Acelerated;
        }

        //Rotates the entity towards player if requested
        if (rotateTowardsTarget)
        {
            RotateToTarget();
        }

        //Cinematic set up
        if (linerType == LinerType.Constant)
        {
            SetUpCinematicAttributes();
            //Debug.Log("CienticLiner creado!");
        }

        //Accelerated set up
        else
        {
            //A Cinematic movement needs physics involved
            RB2D = GetComponent<Rigidbody2D>();
            if (RB2D == null)
            {
                RB2D = gameObject.AddComponent<Rigidbody2D>();
            }
            RB2D.freezeRotation = true;
            RB2D.gravityScale = 0;
        }
    }
    void Update()
    {
        //Tengo que devolver cinetica o fisica según toque.
        //Podria tener una capa intermedia que seleccione la movida
        switch (linerType)
        {
            case LinerType.Constant:
                transform.position = CinematicLiner();
                break;
            case LinerType.Acelerated:
                RB2D.velocity = PhysicsLiner();
                float dist = Vector3.Distance(transform.position, targetPoint);
                if (dist < 1.2f)
                {
                    //STOP
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
                break;
        }
    }

    public void SetTargetPosition(Vector2 pos)
    {
        t = 0;
        targetPoint = new Vector3(pos.x, pos.y);
        startPosition = transform.position;

        if (rotateTowardsTarget) RotateToTarget();
    }

    protected void SetLinerType(LinerType lt)
    {
        linerType = lt;
    }


    /// <summary>
    /// This method is called from the enemy engine TODO: ver si lo voy a usar
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetMovement()
    {
        //Debug.Log("[LINER] Get movement from external call...");
        //Tengo que devolver cinetica o fisica según toque.
        Vector2 externalMovement = Vector2.zero;
        switch (linerType)
        {
            case LinerType.Constant:
                externalMovement = CinematicLiner();
                break;
            case LinerType.Acelerated:
                externalMovement = PhysicsLiner();
                break;
        }

        return externalMovement;

    }


    #region Physics Movement

    private Vector2 PhysicsLiner()
    {
        Vector2 dir = targetPoint - transform.position;

        //V = vo + a*t
        t += Time.deltaTime / timeToReachTarget;
        return dir * Acceleration * t;
    }
    #endregion

    #region Cinematic Movement
    /// <summary>
    /// Starts cinematic Liner
    /// </summary>
    private void SetUpCinematicAttributes()
    {
        startPosition = transform.position;
        t = 0;
    }

    /// <summary>
    /// Cinematic movement for a Liner component
    /// </summary>
    /// <returns></returns>
    private Vector2 CinematicLiner()
    {
        t += Time.deltaTime / timeToReachTarget;
        return Vector2.Lerp(startPosition, targetPoint, t);
    }

    #endregion

}