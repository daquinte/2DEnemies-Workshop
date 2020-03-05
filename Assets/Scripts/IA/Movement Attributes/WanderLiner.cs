using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: Comentar y añadir al docu
/// </summary>
public class WanderLiner : Liner
{
    public float randomRadius = 5f;
    public float timeToRefresh = 1f;
   

    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.time;
        if (currentTime >= timeToRefresh)
        {
            SetTargetPosition(Random.insideUnitCircle * randomRadius);
            currentTime = 0;
        }

        //Move according to current liner configuration
        transform.position = GetMovement();
    }
}
