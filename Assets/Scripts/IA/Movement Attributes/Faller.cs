using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy falls from the ceiling onto the ground.
/// </summary>
public class Faller : MonoBehaviour
{
    public float fallSpeed;                             //downwards speed
    public int detectionRange;                          //Range in which the enemy detects the player

    private Vector3 originalPosition;                   //The position the entity will return after falling
    private int playerLayerMask;                        //Player layer for raycast

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        playerLayerMask = (LayerMask.GetMask("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, playerLayerMask);
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
        //transform.Translate(Vector2.down * fallSpeed);
        //iTween.MoveAdd(gameObject, new Vector3(transform.position.x, transform.position.y - fallSpeed), 1f);
        iTween.MoveAdd(gameObject, Vector3.down * fallSpeed, 1f);
        
        //StartCoroutine("ReturnToOriginalPosition");
    }


   IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        transform.Translate(Vector2.up * fallSpeed);
        yield return null;
    }
}
