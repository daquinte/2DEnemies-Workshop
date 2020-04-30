using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbstractChangeDir : MonoBehaviour
{
    //Enumerator to calcule the direction of the entity
    protected enum InitialMovement { Right, Left };

    private InitialMovement initialMovement = InitialMovement.Left;
    public float movementSpeed = 3f;

    private float pMovementSpeed;
    /// <summary>
    /// Setup of the direcion, dependant on he initial movement.
    /// </summary>
    protected void SetupDir()
    {
        //Set the direction (BASE)
        float dir = 0f;

        if (transform.eulerAngles.y == 0)
        {
            dir = (initialMovement == InitialMovement.Left) ? -1f : 1f;
        }
        else if (transform.eulerAngles.y == 180)
        {
            dir = (initialMovement == InitialMovement.Left) ? 1f : -1f;

        }
        pMovementSpeed = movementSpeed * dir;
    }


    /// <summary>
    /// Moves the entity, always to the right but modified by the movementSpeed´s
    /// magnitude and it being positive or negative.
    /// </summary>
    protected void MoveForward()
    {
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
}
