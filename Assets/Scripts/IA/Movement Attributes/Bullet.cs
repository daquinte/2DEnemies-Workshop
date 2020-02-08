using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves in a straight line in a given direction: left or right
/// </summary>
public class Bullet : AbstractChangeDir
{

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public Vector2 GetBulletMovement()
    {
        return Vector2.left * movementSpeed * Time.deltaTime;
    }


}
