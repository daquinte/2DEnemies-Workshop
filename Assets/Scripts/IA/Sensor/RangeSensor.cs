using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class RangeSensor : Sensor 
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        OnSensorActive();
    }

    override public void OnSensorActive()
    {
        foreach(MonoBehaviour monoBehaviour in components)
        {
            Debug.Log(monoBehaviour.GetType()); //Devuelve "Jumper"
            monoBehaviour.enabled = true;       //¡Y se activa!
        }
    }
}
