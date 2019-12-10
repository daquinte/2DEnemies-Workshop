using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerator to calcule the direction of the entity
public enum InitialMovement { Right, Left };

public class AbstractChangeDir : MonoBehaviour
{
    public InitialMovement initialMovement;
    public float movementSpeed = 0.1f;

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
        movementSpeed *= dir;
    }


    /// <summary>
    /// Moves the entity, always to the right but modified by the movementSpeed´s
    /// magnitude and it being positive or negative.
    /// </summary>
    protected void MoveForward()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the gameObject 180º in Y axis. 
    /// </summary>
    protected void ChangeDir()
    {
        transform.Rotate(Vector2.up, 180);
    }


    /// <summary>
    /// Method that will be overrrided in each sensor
    /// </summary>
    protected virtual void Check() {}
}
