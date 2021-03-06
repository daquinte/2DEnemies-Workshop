﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Sensors/AreaSensor")]

public class AreaSensor : Sensor
{
    
    [Tooltip("This position is relagtive to the center of the entity")]
    public Vector2 LocalAreaPosition;
    public Vector2 AreaSize;

    private BoxCollider2D sensorCollider;
    private const float BoxColliderModifier = 0.2f;
    private const float defaultSize = 1;

    private void Start()
    {
        SetupAreaSensor();
        
    }

    private void Update()
    {
        base.CheckForDeactivateStateChange();
    }

    /// <summary>
    /// Sets up the area taking into consideration the user´s input
    /// </summary>
    private void SetupAreaSensor()
    {
        if(AreaSize.x == 0 || AreaSize.y == 0)
        {
            Debug.LogWarning("[Area Sensor] Width or Heigh can´t be zero! Setting it to a default size...");
            AreaSize.x = AreaSize.x == 0 ? defaultSize : AreaSize.x;
            AreaSize.y = AreaSize.y == 0 ? defaultSize : AreaSize.y;
        }
        //1 -> 0.2
        //aP -> x
        sensorCollider = gameObject.AddComponent<BoxCollider2D>();
        sensorCollider.isTrigger = true;
        sensorCollider.offset = new Vector2(LocalAreaPosition.x * BoxColliderModifier, LocalAreaPosition.y * BoxColliderModifier);
        sensorCollider.size = new Vector2(AreaSize.x * BoxColliderModifier, AreaSize.y * BoxColliderModifier);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyEngine enemyEngine = GetComponent<EnemyEngine>();
        if (enemyEngine != null)
        {
            if (!collision.gameObject.layer.Equals(enemyEngine.GetGroundLayer()))
            {
                sensorActive = true;
                OnSensorActive();
            }
        }
        else Debug.LogWarning("No components detected! A sensor can´t work without at least one");


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyEngine enemyEngine = GetComponent<EnemyEngine>();
        if (enemyEngine != null)
        {
            if (sensorActive)
            {
                if (!collision.gameObject.layer.Equals(enemyEngine.GetGroundLayer()))
                {
                    sensorActive = false;
                    OnSensorExit();
                }
            }
        }
    }

    /*Senson Methods*/
    protected override void OnSensorActive()
    {
        base.OnSensorActive();
    }


    protected override void OnSensorExit()
    {
        base.OnSensorExit();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector2 boxPosition = (Vector2)transform.position + LocalAreaPosition;
        Gizmos.DrawWireCube(boxPosition, AreaSize);
    }

}
