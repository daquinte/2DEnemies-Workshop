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

    [SerializeField]
    private LinerType linerType;

    private Liner liner;
    private float currentTime;


    void Start()
    {
        currentTime = 0f;

        liner = GetComponent<Liner>();
        if(liner == null) { 
            liner = gameObject.AddComponent<Liner>() as Liner;
            SetLinerType(linerType);
        }
        if (rotateTowardsTarget)
            RotateToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.time;
        if(currentTime >= timeToRefresh)
        {
            SetTargetPosition(enemyEngine.enemyTarget.transform.position);
        }

        //Move according to current liner configuration
        transform.position = GetMovement();
    }
}

//TODO: TEST