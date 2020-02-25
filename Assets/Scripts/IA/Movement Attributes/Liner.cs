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

    public float timeToReachTarget;


    [SerializeField]
    private LinerType linerType = LinerType.Constant;

    protected enum LinerType { Acelerated, Constant };

    protected float t;                                    //temp t value for steering
    protected Vector3 targetPoint;                        //The end point of the line

    private CineticLiner testCinetic;

    // Start is called before the first frame update
    void Start()
    {     
        targetPoint = enemyEngine.GetTargetPosition();
        if (rotateTowardsTarget)
        {
            RotateToTarget();
        }

        if(linerType == LinerType.Constant)
        {
            testCinetic = new CineticLiner();
            Debug.Log("CienticLiner creado!");
        }
    }

    void Update()
    {
        //Tengo que devolver cinetica o fisica según toque.
        //Podria tener una capa intermedia que seleccione la movida
        switch (linerType)
        {
            case LinerType.Constant:
                //TODO: Resetea la posicion al 0,0 al iniciar
                transform.position = testCinetic.GetMovement(targetPoint, timeToReachTarget);
                break;
        }
    }

    public void SetTargetPosition(Vector2 pos)
    {
        t = 0;
        targetPoint = new Vector3(pos.x, pos.y);
    }

    protected void SetLinerType(LinerType lt)
    {
        linerType = lt;
    }
    

    /// <summary>
    /// This method is called from the enemy engine and it does hell of a lot of things!!!!
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetMovement()
    {
        Debug.Log("Si sale esto, está mal");
        //Tengo que devolver cinetica o fisica según toque.
        Vector2 aaa = Vector2.zero;
        switch (linerType)
        {
            case LinerType.Constant:
                aaa = testCinetic.GetMovement(targetPoint, timeToReachTarget);
                break;
        }

        return aaa;

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

}