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
    public float randomRadius = 10f;
    public float timeToRefresh = 3f;
   

    private float currentTime;
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
            SetTargetPosition(Random.insideUnitCircle * randomRadius);
            currentTime = 0;
        }

        //Move according to current liner configuration
        transform.position = GetMovement();
    }
}
