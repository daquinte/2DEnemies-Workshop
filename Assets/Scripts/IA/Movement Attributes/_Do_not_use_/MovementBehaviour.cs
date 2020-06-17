using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyEngine))]


/// <summary>
/// A Movement Behaviour defines one specific type of movement 
/// Its individual acceleration to an object can be combined with other movement behaviours
/// So that a complex movement is created.
/// </summary>
public abstract class MovementBehaviour : MonoBehaviour
{

    protected EnemyEngine enemyEngine;                  //Instance of the enemy engine

    //public bool SetInactiveAfterStart = false;               //Should this behaviour shut itself up after Start()? Used in all Sensors, to set up the behaviour when needed.
    
    // Start is called before the first frame update
    public void Awake()
    {

        enemyEngine = GetComponent<EnemyEngine>();
        //enemyEngine.RegistrerBehaviour(this);
    
    }

    //Get the movement
    public abstract Vector2 GetMovement();

    /// <summary>
    /// Rotates the entity to face the current target.
    /// Common to all liner components.
    /// </summary>
    protected void RotateToTarget()
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(enemyEngine.GetTargetPosition().y - transform.position.y, enemyEngine.GetTargetPosition().x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object

        if (enemyEngine.GetTargetPosition().x > transform.position.x)
        {
            transform.rotation = (Quaternion.Euler(0, 180, -AngleDeg));
        }
        else transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 180);
    }

    protected Vector2 lastMovement; //???

    private void OnEnable()
    {
    }
}
