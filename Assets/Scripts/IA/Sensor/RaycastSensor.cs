using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSensor : Sensor
{

    [Tooltip("Position the ray will fire from")]
    public Transform originPoint;

    [Tooltip("Direction the ray will follow")]
    public Vector2 rayDirection;

    public float raycastDistance;

    [Tooltip("Especifies the layer the ray will collide on.")]
    public LayerMask layerMask;


    private int layer;
    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask(LayerMask.LayerToName(layerMask));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D sensorRay = Physics2D.Raycast(originPoint.position, rayDirection, raycastDistance, layerMask);
        if(sensorRay.collider == null)
        {
            sensorActive = true;
        }

        else if (sensorActive && sensorRay.collider != null)
        {
            sensorActive = false;
        }
    }
}
