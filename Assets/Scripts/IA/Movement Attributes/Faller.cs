using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Faller enemy will drop itself down when a certain condition is met.
/// The condition usually is given by the detection range.
/// </summary>
public class Faller : MonoBehaviour
{
    public int gravity;                                 //Gravity that will be applied for the fall.
    public int detectionRange;                          //Range in which the enemy detects the player
    public float timeBeforeFall = 0.1f;                 //Time, in unscaled seconds, the enemy will wait before the fall

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
        //RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayerMask);
        RaycastHit2D enemyRay = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.down, detectionRange, playerLayerMask);
        if (enemyRay.collider != null)
        {
            Debug.Log("AAAAAAAAAAA");
            PlayerManager PM = enemyRay.collider.gameObject.GetComponent<PlayerManager>();
            if (PM != null)
            {
                StartCoroutine("FallDown");
            }
        }

    }

    IEnumerator FallDown()
    {
        yield return new WaitForSecondsRealtime(timeBeforeFall);
        ModifyGravityScale(gravity);
            
   
    }

    void ModifyGravityScale(int mod)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = mod;
        }
    }

    /// <summary>
    /// Gizmos for the Faller behaviour
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - detectionRange));
    }
}
