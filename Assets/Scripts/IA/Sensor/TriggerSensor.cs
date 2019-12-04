using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PresetTrigger { None, Up, Down, Left, Right, BothSides}


public class TriggerSensor : Sensor
{

    [Tooltip("The sensor will set the Triggers accordingly, based on Renderer size")]
    public PresetTrigger presetTrigger;

    public BoxCollider2D[] customBoxColliders;

    // Start is called before the first frame update
    void Start()
    {
        if (presetTrigger != PresetTrigger.None)
            SetPresetTriggers();
    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnSensorActive();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnSensorExit();
    }

    private void SetPresetTriggers()
    {
        /*
            groundPoint = new GameObject("JumperGroundPoint");
            Renderer rend = GetComponent<Renderer>();
            groundPoint.transform.position = new Vector2(transform.position.x, rend.bounds.min.y);
            groundPoint.transform.parent = gameObject.transform;
         */
        switch (presetTrigger)
        {
            case PresetTrigger.Up:
                break;

            case PresetTrigger.Down:
                break;

            case PresetTrigger.Left:
                break;

            case PresetTrigger.Right:
                break;

            case PresetTrigger.BothSides:
                break;
        }
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
