using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Father of all the Senson classes.
/// </summary>
public class Sensor : MonoBehaviour
{

    /*Attributes*/

    [Tooltip("Components list you would like to enable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List <MonoBehaviour> activateComponents;

    [Tooltip("Components list you would like to disable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List <MonoBehaviour> deactivateComponents;

    protected bool sensorActive;        //Sometimes I just want the sensor to track this information so any components (previously active) can ask.

    private void Awake()
    {
        //We have to make sure that all components that are meant to be activated
        //are deactivated by default
        foreach (MonoBehaviour monoBehaviour in activateComponents)
        {
            monoBehaviour.enabled = false;
        }
    }
    /*Methods*/
    public bool GetSensorActive()
    {
        return sensorActive;
    }

    protected virtual void OnSensorActive() {

        sensorActive = true;
        foreach (MonoBehaviour monoBehaviour in activateComponents)
        {
            monoBehaviour.enabled = true;
        }
    }
    protected virtual void OnSensorExit() { sensorActive = false; }

}
