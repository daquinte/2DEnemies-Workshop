using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSensor : Sensor
{

    [Tooltip("Position the ray will fire from")]
    public Vector3 originPoint;

    [Tooltip("Direction the ray will follow")]
    public Vector2 rayDirection;

    private int layer;
    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D sensorRay = Physics2D.Raycast(originPoint, rayDirection, rayDirection.magnitude, layer);
        Debug.Log(rayDirection.magnitude);
        Debug.DrawRay(originPoint, rayDirection, Color.green);
        if(sensorRay.collider != null)
        {
            sensorActive = true;
            OnSensorActive();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(originPoint, rayDirection);
    }
}
