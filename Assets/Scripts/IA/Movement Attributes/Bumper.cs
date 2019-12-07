using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerSensor))]                      //A Bumper entity needs a sensor to change direction
public class Bumper : Pacer
{
    // Start is called before the first frame update
    void Start()
    {
        float dir = 0;
        if (transform.eulerAngles.y == 0)
        {
            dir = (initialMovement == InitialMovement.Left) ? -1f : 1f;
        }

        movementSpeed *= dir;
    }

    // Update is called once per frame
    void Update()
    {
        base.Move();
        Check();
    }

    protected override void Check()
    {
        Sensor triggerSensor = GetComponent<Sensor>();
        if (triggerSensor.GetSensorActive())
        {
            base.ChangeDir();
        }
    }
}
