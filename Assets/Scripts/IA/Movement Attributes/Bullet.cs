using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves in a straight line in a given direction: left or right
/// </summary>
public class Bullet : MonoBehaviour
{

    public int movementSpeed;

    //From the public enum in AbstractDir
    public InitialMovement initialMovement; 
    
    public void ChangeDirection(InitialMovement initialM)
    {
        initialMovement = initialM;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialMovement == InitialMovement.Left)
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

    }
}
