using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AreaSensor : Sensor
{
    public BoxCollider2D[] AreaList;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sensorActive = true;
        OnSensorActive();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sensorActive = false;
        OnSensorExit();
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
        foreach (BoxCollider2D c in AreaList)
        {
            Vector2 center = new Vector2(c.transform.position.x, c.transform.position.y);
            Gizmos.DrawWireCube(center, c.size);
        }
    }

}
