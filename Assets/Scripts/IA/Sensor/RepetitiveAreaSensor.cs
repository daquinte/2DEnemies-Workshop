using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This sensor allows the user to turn on and off components on a cyclical manner.
/// The listed components will 
/// </summary>
public class RepetitiveAreaSensor : AreaSensor
{
    [Tooltip("Time, in seconds, in which the sensor will cycle")]
    public float repeatTime = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.layer.Equals(GameManager.instance.GetGroundLayer()))
        {
            sensorActive = true;
            StartCoroutine(RepetitiveAreaCorroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.layer.Equals(GameManager.instance.GetGroundLayer()))
        {
            sensorActive = false;
            OnSensorExit();
            StopAllCoroutines();
        }
    }

    IEnumerator RepetitiveAreaCorroutine()
    {
        while (sensorActive)
        {
            OnSensorActive();
            yield return new WaitForSeconds(repeatTime);
            OnSensorExit();
            yield return null;
        }
    }
}
