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
                                                        // Start is called before the first frame update
    public void Start()
    {

        enemyEngine = GetComponent<EnemyEngine>();
        enemyEngine.RegistrerBehaviour(this);
    
    }

    public abstract Vector2 GetMovement();

    protected Vector2 lastMovement; //???
}
