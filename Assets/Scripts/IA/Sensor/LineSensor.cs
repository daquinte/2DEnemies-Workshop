using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Sensors/LineSensor")]
public class LineSensor : Sensor
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        LineDirection = LineDirection.normalized;
    }
#endif

    //[Tooltip("Position the ray will fire from")]
    //public Vector2 LocalRaycastPoint;
    [Space(10)]
    [Tooltip("Allow the line of sight to see through terrain?")]
    public bool SeeThroughWalls = false;

    [Tooltip("Direction the ray will follow")]
    public Vector2 LineDirection;

    [Tooltip("Raycast magnitude")]
    public float LineDistance= 3;

    private bool turnOffGizmos = false;

    // Start is called before the first frame update
    void Start()
    {
        turnOffGizmos = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.CheckForDeactivateStateChange();
        List<RaycastHit2D> rayCastInfo = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        int sensorRay = Physics2D.Raycast(transform.position, LineDirection, contactFilter2D, rayCastInfo, LineDistance);
        Debug.DrawRay(transform.position, LineDirection * LineDistance, Color.green);
       

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
                else 
                {
                    EnemyEngine enemyEngine = GetComponent<EnemyEngine>();
                    if(enemyEngine != null)
                    {
                        if (rayCastInfo[i].collider.gameObject.layer == enemyEngine.GetPlayerLayer())
                        {
                            OnSensorDetection();
                            //Stop looking
                            stop = true;
                        }                       
                    }
                    else Debug.LogWarning("No components detected! A sensor can´t work without at least one");

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
            Gizmos.color = Color.cyan;
            Vector2 GizmosRayDirection = (Vector2)transform.position + LineDirection * LineDistance;
            Gizmos.DrawLine(transform.position, GizmosRayDirection);
        }
    }
}
