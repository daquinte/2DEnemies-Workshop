﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AreaSensor : Sensor
{
    
    [Tooltip("This position is relagtive to the center of the entity")]
    public Vector2 LocalAreaPosition;
    public Vector2 AreaWidth;

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

    private void SetupAreaSensor()
    {
        if(AreaWidth.x == 0 || AreaWidth.y == 0)
        {
            Debug.LogWarning("[Area Sensor] Width or Heigh can´t be zero! Setting it to a default size...");
            AreaWidth.x = AreaWidth.x == 0 ? defaultSize : AreaWidth.x;
            AreaWidth.y = AreaWidth.y == 0 ? defaultSize : AreaWidth.y;
        }
        //1 -> 0.2
        //aP -> x
        sensorCollider = gameObject.AddComponent<BoxCollider2D>();
        sensorCollider.isTrigger = true;
        sensorCollider.offset = new Vector2(LocalAreaPosition.x * BoxColliderModifier, LocalAreaPosition.y * BoxColliderModifier);
        sensorCollider.size = new Vector2(AreaWidth.x * BoxColliderModifier, AreaWidth.y * BoxColliderModifier);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.gameObject.layer.Equals(GameManager.instance.GetGroundLayer()))
        {
            sensorActive = true;
            OnSensorActive();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (sensorActive) {
            if (!collision.gameObject.layer.Equals(GameManager.instance.GetGroundLayer()))
            {
                sensorActive = false;
                OnSensorExit();
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
        Vector2 boxPosition = new Vector2(transform.position.x + LocalAreaPosition.x, transform.position.y + LocalAreaPosition.y);
        Gizmos.DrawWireCube(boxPosition, AreaWidth);
    }

}
