using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AreaSensor : Sensor
{
    
    public Vector2 AreaPosition;
    public Vector2 AreaWidth;

    private BoxCollider2D sensorCollider;
    private const float BoxColliderModifier = 0.2f;

    private void Start()
    {
        SetupAreaSensor();
    }

    private void SetupAreaSensor()
    {
        //1 -> 0.2
        //aP -> x
        sensorCollider = gameObject.AddComponent<BoxCollider2D>();
        sensorCollider.isTrigger = true;
        sensorCollider.offset = new Vector2(AreaPosition.x * BoxColliderModifier, AreaPosition.y * BoxColliderModifier);
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
        if (!collision.gameObject.layer.Equals(GameManager.instance.GetGroundLayer()))
        {
            sensorActive = false;
            OnSensorExit();
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
        Gizmos.DrawWireCube(AreaPosition, AreaWidth);
    }

}
