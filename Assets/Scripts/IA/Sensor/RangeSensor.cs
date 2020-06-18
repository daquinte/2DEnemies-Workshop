using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum RangeSensorType { Horizontal, Vertical, DistanceBased }

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Sensors/RangeSensor")]

/// <summary>
/// This class works as a distance or range sensor between this entity and some target GameObject
/// When the distance is closer than a given value, either in one of the axis or any given distance, 
/// the sensor will trigger a customizable response.
/// </summary>

public class RangeSensor : Sensor
{
    [Tooltip("Especifies the sensor range´s type.")]
    public RangeSensorType sensorType;

    [Tooltip("Range, in Unity units, the sensor will value")]
    public float detectionRange = 3.0f;



    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        target = GameManager.instance.GetLevelManager().GetPlayer();
        if (target == null)
        {
            Debug.LogWarning("Player is null! Please, create a player if there is none.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        base.CheckForDeactivateStateChange();
        float distance = 0;
        switch (sensorType)
        {
            case RangeSensorType.Horizontal:
                distance = Mathf.Abs(transform.position.x - target.transform.position.x);                
                break;
            case RangeSensorType.Vertical:
                distance = Mathf.Abs(target.transform.position.y - transform.position.y);               
                break;
            case RangeSensorType.DistanceBased:
                distance = Vector2.Distance(transform.position, target.transform.position);              
                break;
        }

        if (!sensorActive && distance < detectionRange)
            OnSensorActive();

        else if (sensorActive && distance > detectionRange) OnSensorExit();

    }

    override protected void OnSensorActive()
    {
        base.OnSensorActive();
    }

    override protected void OnSensorExit()
    {
        base.OnSensorExit();

        /* 
         foreach (MonoBehaviour monoBehaviour in deactivateComponents)
         {
             monoBehaviour.enabled = true;      
         }*/ //No neceseariamente quiero activar todos los componentes que quiero desactivar.
    }


    /// <summary>
    /// Gizmos for the editor.
    /// We draw a line to 
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        float x = transform.position.x;
        float y = transform.position.y;
        if (sensorType == RangeSensorType.Horizontal)
        {
            Gizmos.DrawLine(transform.position, new Vector2(x + detectionRange, y));
            Gizmos.DrawLine(transform.position, new Vector2(x - detectionRange, y));
        }

        if (sensorType == RangeSensorType.Vertical)
        {
            Gizmos.DrawLine(transform.position, new Vector2(x, y + detectionRange));
            Gizmos.DrawLine(transform.position, new Vector2(x, y - detectionRange));
        }

        if (sensorType == RangeSensorType.DistanceBased)
        {
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    }
}
