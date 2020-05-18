using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSensor : Sensor
{

    //[Tooltip("Position the ray will fire from")]
    //public Vector2 LocalRaycastPoint;

    [Tooltip("Direction the ray will follow")]
    public Vector2 RayDirection;

    private int layer;
    private bool turnOffGizmos = false;

    // Start is called before the first frame update
    void Start()
    {
        layer = GameManager.instance.GetPlayerLayer();
        turnOffGizmos = true;
    }

    // Update is called once per frame
    void Update()
    {
        //We update the rayposition
        //Vector2 RayPosition = new Vector2(transform.position.x + LocalRaycastPoint.x, transform.position.y + LocalRaycastPoint.y);

        RaycastHit2D sensorRay = Physics2D.Raycast(transform.position, RayDirection, RayDirection.magnitude, layer);
        Debug.DrawRay(transform.position, RayDirection, Color.green);
        if (sensorRay.collider != null)
        {
            sensorActive = true;
            OnSensorActive();
        }
        else
        {
            if (sensorActive)
            {
                sensorActive = false;
                OnSensorExit();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!turnOffGizmos)
        {
            Gizmos.color = Color.red;
            Vector2 GizmosRayDirection = new Vector2(transform.position.x + RayDirection.x, transform.position.y + RayDirection.y);
            Gizmos.DrawLine(transform.position, GizmosRayDirection);
        }
    }
}
