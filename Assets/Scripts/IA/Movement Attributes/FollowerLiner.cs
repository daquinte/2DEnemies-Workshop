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
    [Tooltip("Time that this component will wait " +
        "before sending player pos to Liner component")]
    public float timeToRefresh;

    private float currentTime;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.time;
        if (currentTime >= timeToRefresh)
        {
            SetTargetPosition(enemyEngine.enemyTarget.transform.position);
            currentTime = 0;
        }

        //Move according to current liner configuration
        transform.position = GetMovement();
    }
}

//TODO: TEST