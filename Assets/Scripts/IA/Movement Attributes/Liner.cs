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
    public bool rotateTowardsTarget;

    [Tooltip("Time to reach the target Point")]
    public float timeToReachTarget;


    [SerializeField]
    private LinerType linerType = LinerType.Constant;

    protected enum LinerType { Acelerated, Constant };

    protected float t;                                    //temp t value for steering
    protected Vector3 targetPoint;                        //The end point of the line

    //Cinematic attributes
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = enemyEngine.GetTargetPosition();
        if (rotateTowardsTarget)
        {
            RotateToTarget();
        }

        if (linerType == LinerType.Constant)
        {
            SetUpCinematicAttributes();
            Debug.Log("CienticLiner creado!");
        }
        else { enemyEngine.RegistrerBehaviour(this); }
    }

    void Update()
    {
        //Tengo que devolver cinetica o fisica según toque.
        //Podria tener una capa intermedia que seleccione la movida
        switch (linerType)
        {
            case LinerType.Constant:
                //TODO: Resetea la posicion al 0,0 al iniciar
                transform.position = CinematicLiner();
                break;
            case LinerType.Acelerated:

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
        Debug.Log("[LINER] Get movement from external call...");
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
        //TODO: Ajustar directamente la posición: si estás dentro de un rango X, fuerzas que se ponga por código 
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
            t += Time.deltaTime / timeToReachTarget;
            steering = Vector3.Lerp(steering, desiredVelocity, t);
            lastMovement = steering;
        }
        else
        {
            transform.position = enemyEngine.enemyTarget.transform.position;
            lastMovement = Vector3.zero;
        }
        return lastMovement;
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

    /// <summary>
    /// Rotates the entity to face the current target.
    /// Common to all liner components.
    /// </summary>
    protected void RotateToTarget()
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

}