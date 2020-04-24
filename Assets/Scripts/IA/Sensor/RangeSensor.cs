using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum RangeSensorType { Horizontal, Vertical, DistanceBased }
public enum Target { Player, Custom }

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
    public float detectionRange = 1.0f;


    public Target targetType;


    [Tooltip("The target in case you don´t want the target to be the Player. If that´s so, please select Custom in the Target Type")]
    public GameObject customTarget;


    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if (targetType == Target.Player)
        {
            target = GameManager.instance.GetLevelManager().GetPlayer();
            if (target == null)
            {
                Debug.LogWarning("Player is null! Please, create a player if there is none.");
            }
        }      
        else { target = customTarget; }
    }

    // Update is called once per frame
    void Update()
    {
        switch (sensorType)
        {
            case RangeSensorType.Horizontal:
                if (Mathf.Abs(transform.position.x - target.transform.position.x) < detectionRange)
                    OnSensorActive();
                else if (sensorActive) OnSensorExit();
                break;
            case RangeSensorType.Vertical:
                if (Mathf.Abs(transform.position.y - target.transform.position.y) > detectionRange) //TODO: mayor?
                    OnSensorActive();
                else if (sensorActive) OnSensorExit();
                break;
            case RangeSensorType.DistanceBased:
                if (Vector2.Distance(transform.position, target.transform.position) < detectionRange)
                    OnSensorActive();
                else if (sensorActive) OnSensorExit();
                break;
        }

    }

    override protected void OnSensorActive()
    {
        base.OnSensorActive();  
    }

    override protected void OnSensorExit()
    {
        foreach (MonoBehaviour monoBehaviour in activateComponents)
        {
            monoBehaviour.enabled = false;
        }

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
        Gizmos.color = Color.green;
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

        if(sensorType == RangeSensorType.DistanceBased)
        {
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    }
}
