using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves in a straight line in a given direction: left or right
/// </summary>
public class Bullet : MonoBehaviour
{

    public int movementSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
    }
}
