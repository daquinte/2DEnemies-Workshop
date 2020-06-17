using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Follower")]

/// <summary>
/// This Component provides a Liner with the target position when the timer states so
/// The Liner can decide how it wants to move the entity, so a Liner component is required 
/// but you dont need to include one beforehand
/// </summary>
public class FollowerLiner : Liner
{
    [Space(5)]
    [Tooltip("Time that this component will wait before sending player position to Liner component")]
    public float timeToRefresh = 0.4f;

    private bool updatePlayerPosition = true;
    // Update is called once per frame
    void Update()
    {
       
        if (updatePlayerPosition)
        {
            SetTargetPosition(enemyEngine.GetTargetPosition());
            updatePlayerPosition = false;
            StartCoroutine(SetTargetPositionAfterSeconds());
        }

        //Move according to current liner configuration
        if (linerType == LinerType.Acelerated)
        {
            GetComponent<Rigidbody2D>().velocity = GetMovement();
        }
        else if (linerType == LinerType.Constant)
        {
            transform.position = GetMovement();
        }
    }

    IEnumerator SetTargetPositionAfterSeconds()
    {
        yield return new WaitForSecondsRealtime(timeToRefresh);
        updatePlayerPosition = true;
    }
}

