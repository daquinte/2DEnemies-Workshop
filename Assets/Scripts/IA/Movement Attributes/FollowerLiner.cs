using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Component provides a Liner with the target position when the timer states so
/// The Liner can decide how it wants to move the entity, so a Liner component is required 
/// but you dont need to include one beforehand
/// </summary>
public class FollowerLiner : Liner
{
    [Space(5)]
    [Tooltip("Time that this component will wait " +
        "before sending player position to Liner component")]
    public float timeToRefresh = 0.4f;

    private float currentTime = 2f;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.time;
        if (currentTime >= timeToRefresh)
        {
            SetTargetPosition(enemyEngine.GetTargetPosition());
            currentTime = 0;
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
}

