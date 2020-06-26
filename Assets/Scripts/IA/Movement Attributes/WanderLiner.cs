using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Wander")]

/// <summary>
/// Wanders inside a customizable area, changing a target position in the time given.
/// </summary>
public class WanderLiner : Liner
{

#if UNITY_EDITOR
    private void OnValidate()
    {
        target = null;                              //This gameobject cant have a target!
    }
#endif

    [Tooltip("Area in which the wander can move")]
    public float areaRadius = 10f;

    [Tooltip("Time that will wait after reaching destination and setting a new waypoint")]
    public float timeToRefresh = 3f;
   

    private float currentTime;                      //Current time for internal time check purposes


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToRefresh)
        {
            //Set a new position in the internal Liner
            SetTargetPosition(Random.insideUnitCircle * areaRadius);
            currentTime = 0;
        }

        //Move according to current liner configuration
        transform.position = GetMovement();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
