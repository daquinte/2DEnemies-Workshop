using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbstractChangeDir : MovementBehaviour
{
    //Public movement speed for this components
    public float movementSpeed = 3f;

    //Enumerator to calcule the direction of the entity
    protected enum InitialMovement { Right, Left };

    private InitialMovement initialMovement = InitialMovement.Left;


    protected float pMovementSpeed;
    protected float pMovementDir = 0f;

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
