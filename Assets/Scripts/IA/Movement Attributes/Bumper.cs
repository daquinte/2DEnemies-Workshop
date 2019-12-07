using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerSensor))]         //A Bumper entity needs a sensor to change direction
public class Bumper : AbstractChangeDir
{
    // Start is called before the first frame update
    void Start()
    {
        SetupDir();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        Check();
    }

    protected override void Check()
    {
        Sensor triggerSensor = GetComponent<Sensor>();
        if (triggerSensor.GetSensorActive())
        {
            ChangeDir();
        }
    }
}
