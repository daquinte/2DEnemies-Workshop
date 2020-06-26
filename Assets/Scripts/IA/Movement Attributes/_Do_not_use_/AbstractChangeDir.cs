using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is an abstract class that defines the common parts of all movement components
/// that imply a straight movement and a change of direction
/// </summary>
public class AbstractChangeDir : MovementBehaviour
{
    //Public movement speed for this components
    [Tooltip("Movement speed of this movement")]
    public float movementSpeed = 3f;

    //Enumerator to set the direction of the entity
    protected enum InitialMovement { Right, Left };


    /// <summary>
    /// Sets the initial movement. It works, but gives some unexpected results in the Pacer behaviour.
    /// Uncomment the [SerializeField] attribute if selecting initial movement is desirable, otherwise it will move left by default
    /// </summary>
    //[SerializeField] 
    private InitialMovement initialMovement = InitialMovement.Left;


    protected float pMovementSpeed;            //Private movement speed, so that the movement speed is in absolute value
    protected float pMovementDir = 0f;         //Private direction modificator

    /// <summary>
    /// Setup of the direcion, dependant on he initial movement.
    /// </summary>
    protected void SetupDir()
    {

        if (transform.eulerAngles.y == 0)
        {
            pMovementDir = (initialMovement == InitialMovement.Left) ? -1f : 1f;
        }
        else if (transform.eulerAngles.y == 180)
        {
            pMovementDir = (initialMovement == InitialMovement.Left) ? 1f : -1f;

        }
        pMovementSpeed = Mathf.Abs(movementSpeed) * pMovementDir;
    }


    /// <summary>
    /// Moves the entity, always to the right but modified by the movementSpeed´s
    /// magnitude and it being positive or negative.
    /// </summary>
    protected void MoveForward()
    {
        if(pMovementSpeed != movementSpeed)
        {
            pMovementSpeed = Mathf.Abs(movementSpeed) * pMovementDir;
        }

        transform.Translate(Vector2.right * pMovementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the gameObject 180º in Y axis. 
    /// </summary>
    protected void ChangeDir()
    {
        transform.Rotate(Vector2.up, 180);
    }


    /// <summary>
    /// Method that will be overrided
    /// </summary>
    protected virtual void Check() {}

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }
}
