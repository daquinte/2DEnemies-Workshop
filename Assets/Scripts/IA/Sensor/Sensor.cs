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
    public List<MonoBehaviour> activateComponents;

    public bool CopyComponentsToDeactivateList;
    [Tooltip("Components list you would like to disable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List<MonoBehaviour> deactivateComponents;

    [Space(2)]


    protected bool sensorActive;        //Sometimes I just want the sensor to track this information so any components (previously active) can ask.

    private void Awake()
    {
        //We have to make sure that all components that are meant to be activated
        //are deactivated by default
        foreach (MonoBehaviour movementBehaviour in activateComponents)
        {
            //movementBehaviour.SetInactiveAfterStart = true;
            movementBehaviour.enabled = false;
            if (movementBehaviour.GetComponent<Rigidbody2D>() != null)
            {
                movementBehaviour.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            if (CopyComponentsToDeactivateList)
            {
                deactivateComponents.Add(movementBehaviour);
            }

        }
    }
    /*Methods*/

    protected virtual void OnSensorActive()
    {

        sensorActive = true;
        foreach (MonoBehaviour movementBehaviour in activateComponents)
        {
            movementBehaviour.enabled = true;
            //movementBehaviour.SetInactiveAfterStart = false;   //It can start as intended
        }
    }
    protected virtual void OnSensorExit()
    {
        sensorActive = false;
        foreach (MonoBehaviour movementBehaviour in deactivateComponents)
        {
            movementBehaviour.enabled = false;
        }
    }

}
