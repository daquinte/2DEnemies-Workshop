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
    [Tooltip("Autofill all movement components in this sensor´s activateComponents list")]
    public bool autoFill;

    [Tooltip("Components list you would like to enable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List<MonoBehaviour> activateComponents;

    public bool AutoFillDeactivateList;
    [Tooltip("Components list you would like to disable when trigger is active. Enter a size and drag the components from this inspector window")]
    public List<MonoBehaviour> deactivateComponents;

    [Space(2)]

    protected bool sensorActive;        //Sometimes I just want the sensor to track this information so any components (previously active) can ask.

    private void Awake()
    {

        if (!autoFill && activateComponents.Count == 0)
        {
            Debug.LogWarning("[Sensor] Your activated component´s size seems to be 0. Autofill is being activated by default!");
            autoFill = true;
        }
       
        if (!autoFill)
        {
            foreach (MonoBehaviour movementBehaviour in activateComponents)
            {
                if (movementBehaviour != null)
                {
                    ComponentSetup(movementBehaviour);
                }
                else
                {
                    Debug.Log("[Sensor] Size is bigger than the listed components. Please, add them to the list in the editor or use Auto Fill to add them automatically.");
                }
            }
        }
        else
        {
            //Everything that seems like a movement component goes into the list
            //That includes Abstract dir and Movement
            if(activateComponents.Count != 0)
            {
                Debug.LogWarning("[Sensor] Autofill is enabled, clearing previous list before adding components...");
                activateComponents.Clear();
            }

            foreach (MonoBehaviour movementBehaviour in GetComponents<MonoBehaviour>())
            {

                if (!typeof(Sensor).IsAssignableFrom(movementBehaviour.GetType())
                    &&
                    !typeof(EnemyEngine).IsAssignableFrom(movementBehaviour.GetType()))
                {
                    activateComponents.Add(movementBehaviour);
                    ComponentSetup(movementBehaviour);

                }
            }
        }
    }
    /*Methods*/

    private void ComponentSetup(MonoBehaviour movementBehaviour)
    {
        //We have to make sure that all components that are meant to be activated
        //are deactivated by default
        movementBehaviour.enabled = false;
        if (movementBehaviour.GetComponent<Rigidbody2D>() != null)
        {
            movementBehaviour.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        if (AutoFillDeactivateList)
        {
            deactivateComponents.Add(movementBehaviour);
        }

    }

    protected virtual void OnSensorActive()
    {

        sensorActive = true;
        foreach (MonoBehaviour movementBehaviour in activateComponents)
        {
            if (movementBehaviour != null)
            {
                movementBehaviour.enabled = true;
                //movementBehaviour.SetInactiveAfterStart = false;   //It can start as intended
            }
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
