using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a Rigidbody in order to apply gravity to the gameObject
[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// A Faller enemy will drop itself down when a certain condition is met.
/// The condition usually is given by the detection range.
/// </summary>
public class Faller : MonoBehaviour
{
    public int gravity = 1;                             //Gravity that will be applied for the fall.
    public float timeBeforeFall = 1f;                 //Time, in unscaled seconds, the enemy will wait before the fall

    // Start is called before the first frame update
    void Start()
    {
        ModifyGravityScale(0);
        StartCoroutine(FallDown());

        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    public IEnumerator FallDown()
    {
        yield return new WaitForSeconds(timeBeforeFall);
        //GetComponent<Animator>().SetTrigger("Fall");
        ModifyGravityScale(gravity);
        yield return null;
    }

    private void ModifyGravityScale(int mod)
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
        //Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 2));
    }
}
