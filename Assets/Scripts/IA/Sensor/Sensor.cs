using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Father of all the Senson classes.
/// </summary>
public class Sensor : MonoBehaviour
{

    [Tooltip("Components list you would like to enable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List <MonoBehaviour> activateComponents;

    [Tooltip("Components list you would like to disable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List <MonoBehaviour> deactivateComponents;
    
    protected virtual void OnSensorActive() {

        foreach (MonoBehaviour monoBehaviour in activateComponents)
        {
            monoBehaviour.enabled = true;
        }
    }
    protected virtual void OnSensorExit() { }
}
