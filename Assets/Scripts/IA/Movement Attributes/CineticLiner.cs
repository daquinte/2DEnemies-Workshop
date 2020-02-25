using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tengo que definir la relacion, cinetic liner es llamado desde liner
//Entonces no necesito que sea hijo de Liner....... 
public class CineticLiner : MonoBehaviour
{

    Vector3 startPosition;
    private float t;
    void Start()
    {
        startPosition = transform.position;
        t = 0;
    }
/*
    private void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        targetPoint = destination;
    }*/

    public Vector2 GetMovement(Vector2 targetPoint, float timeToReachTarget)
    {
        t += Time.deltaTime / timeToReachTarget;
        //transform.position = 
        return Vector2.Lerp(startPosition, targetPoint, t);
    }
}
