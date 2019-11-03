using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy moves in a straight line directly to point of the screen.
/// This component changes the direction of the gameObject, instead of moving towards an axis.
/// Usage: A enemy you would want to move from one spot to another, either randomly or between fixed points.
/// 
///  Liner
///Direccion X/Y
///  Speed -> unidades de Unity/segundo
///Cambia direccion!!! (Separara direccion de "movimiento en X eje")
/// -> Una cosa es la direccion del enemigo y otra que se mueva en un determinado eje sin cambiar la rotacion
/// </summary>
public class Liner : MonoBehaviour
{
    public float    movementSpeed = 0.40f;                 //Speed at which the entity moves in Unity units per second
    public float    rotationSpeed = 0.60f;                 //Speed at which the entity rotates in Unity units per second?
    public bool     random;

    public Vector3 end;
    public float travelSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        //rotate to look at the end point
        Vector3 difference = end - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
       

        //move towards the player
        transform.position = Vector3.Lerp(transform.position, end, Time.time * Time.deltaTime);
    }

    public void SetTargetPosition(float x, float y)
    {
        end = new Vector3(x, y);
    }
}
