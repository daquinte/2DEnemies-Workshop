using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy falls from the ceiling onto the ground.
/// </summary>
public class Faller : MonoBehaviour
{
    public float fallSpeed;         //downwards speed
    public int detectionRange;      //Range in which the enemy detects the player

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, Vector2.down, detectionRange);
        if(enemyRay.collider != null)
        {
            PlayerManager PM = enemyRay.collider.gameObject.GetComponent<PlayerManager>();
            if(PM != null)
            {
                FallDown();
            }
        }

    }

    void FallDown()
    {
        transform.Translate(Vector2.down * fallSpeed);
    }
}
