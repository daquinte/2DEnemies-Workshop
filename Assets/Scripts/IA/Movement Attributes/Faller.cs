using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a Rigidbody in order to apply gravity to the gameObject
[RequireComponent(typeof(Rigidbody2D))]

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Faller")]

/// <summary>
/// A Faller enemy will drop itself down.
/// The condition usually is given by the detection range.
/// </summary>
public class Faller : MovementBehaviour
{
    [Tooltip("Downwards speed that will be applied for the fall")]
    public float    appliedGravity = 2.5f;                             //Downwards speed that will be applied for the fall.

    [Tooltip("Time that the enemy will wait before the fall")]
    public float    delayTime = 0.5f;                                  //Time that the enemy will wait before the fall

    // Start is called before the first frame update
    void Start()
    {
        ModifyGravityScale(0);
        StartCoroutine(FallDown());

        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    public IEnumerator FallDown()
    {
        yield return new WaitForSeconds(delayTime);
        //GetComponent<Animator>().SetTrigger("Fall");
        ModifyGravityScale(appliedGravity);
        yield return null;
    }

    /// <summary>
    /// Sets the new gravity scale for this movement
    /// </summary>
    /// <param name="mod">new scale</param>
    private void ModifyGravityScale(float mod)
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 10));
    }

    public override Vector2 GetMovement()
    {
        throw new System.NotImplementedException();
    }
}
