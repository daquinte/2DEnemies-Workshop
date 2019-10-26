using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy falls from the ceiling onto the ground.
/// </summary>
public class Faller : MonoBehaviour
{
    public int gravity;                                 //downwards speed
    public int detectionRange;                          //Range in which the enemy detects the player

    /// ---------------
    /// Private variables
    /// ---------------

    private int playerLayerMask;                        //Player layer for raycast

    // Start is called before the first frame update
    void Start()
    {
        playerLayerMask = (LayerMask.GetMask("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayerMask);
        //RaycastHit2D enemyRay = Physics2D.BoxCast(transform.position, 2, Vector3.down, Vector3.down);
        if(enemyRay.collider != null)
        {
            PlayerManager PM = enemyRay.collider.gameObject.GetComponent<PlayerManager>();
            if(PM != null)
            {
                FallDown();
            }
        }

    }

    IEnumerator FallDown()
    {

        ModifyGravityScale(gravity);
        yield return new WaitForSecondsRealtime(0.2f);
        ModifyGravityScale(0);
        
    }

    void ModifyGravityScale(int mod)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = mod;
        }
    }

}
