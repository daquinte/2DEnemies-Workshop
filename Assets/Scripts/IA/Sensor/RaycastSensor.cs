using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSensor : Sensor
{

    //[Tooltip("Position the ray will fire from")]
    //public Vector2 LocalRaycastPoint;

    [Tooltip("Allow the line of sight to see through terrain?")]
    public bool SeeThroughWalls = false;

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

        if (!SeeThroughWalls)
        {
            List<RaycastHit2D> rayCastInfo = new List<RaycastHit2D>();
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            int sensorRay = Physics2D.Raycast(transform.position, RayDirection, contactFilter2D, rayCastInfo, RayDirection.magnitude);
            Debug.DrawRay(transform.position, RayDirection, Color.green);
            if (sensorRay != 0)
            {
                foreach (RaycastHit2D raycastHit2D in rayCastInfo)
                {
                    //TODO
                }
            }
        }
        else
        {
            RaycastHit2D sensorRayNoWalls = Physics2D.Raycast(transform.position, RayDirection, RayDirection.magnitude, layer);
            Debug.DrawRay(transform.position, RayDirection, Color.green);
            if (sensorRayNoWalls.collider != null)
            {
                OnSensorDetection();
            }
        }
    }

    private void OnSensorDetection()
    {
        if (!sensorActive)
        {
            sensorActive = true;
            OnSensorActive();
        }
        else
        {

            sensorActive = false;
            OnSensorExit();

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
