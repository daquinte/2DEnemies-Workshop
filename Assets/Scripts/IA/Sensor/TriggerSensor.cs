using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerSensor : Sensor
{ 
    public BoxCollider2D[] Colliders;
    
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

    }

}
