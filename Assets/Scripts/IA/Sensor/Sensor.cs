using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Sensor : MonoBehaviour
{

    [Tooltip("Components list you would like to enable. Enter a size and drag the components from this inspector window")]
    public List <MonoBehaviour> activateComponents;

   /* [Tooltip("Components list you would like to disable. Enter a size and drag the components from this inspector window")]
    public List <MonoBehaviour> deactivateComponents;
    */
    public virtual void OnSensorActive() { }
    public virtual void OnSensorExit() { }
}
