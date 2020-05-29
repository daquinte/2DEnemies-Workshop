using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSensor : Sensor
{

    //[Tooltip("Position the ray will fire from")]
    //public Vector2 LocalRaycastPoint;
    [Space(10)]
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
        base.CheckForDeactivateStateChange();
        List<RaycastHit2D> rayCastInfo = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        int sensorRay = Physics2D.Raycast(transform.position, RayDirection, contactFilter2D, rayCastInfo, RayDirection.magnitude);
        Debug.DrawRay(transform.position, RayDirection, Color.green);
       

        if (sensorRay != 0)
        {
            int i = 0;
            bool stop = false;

            while (i < sensorRay && !stop)
            {
                if (rayCastInfo[i].collider.gameObject.layer == GameManager.instance.GetGroundLayer() && !SeeThroughWalls)
                {
                    //Stop looking
                    stop = true;
                }
                else if (rayCastInfo[i].collider.gameObject.layer == GameManager.instance.GetPlayerLayer())
                {
                    OnSensorDetection();
                    //Stop looking
                    stop = true;
                }
                i++;
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
