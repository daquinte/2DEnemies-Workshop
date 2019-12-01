using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TriggerPreset {Up, Down, Left, Right, BothSides}

//public List <BoxCollider> boxColliders;

public class TriggerSensor : Sensor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }



    public override void OnSensorActive()
    {
        base.OnSensorActive();
    }

    public override void OnSensorExit()
    {
       
    }
}
