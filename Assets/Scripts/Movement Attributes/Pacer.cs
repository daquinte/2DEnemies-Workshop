using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy moves in a straight line, but changes direction in response to a trigger.
/// </summary>
public class Pacer : MonoBehaviour
{

    public float movementSpeed = 0.40f;
    private GameObject raycastTransform; //TEST

    private bool MovingRight = true;
  

    //TODO: PENSAR COMO HACER EL TRANSFORM DELANTE Y LANZAR EL RAYO DESDE AHI
    //https://www.youtube.com/watch?v=aRxuKoJH9Y0
    // Start is called before the first frame update
    void Start()
    {

        raycastTransform = new GameObject("test123");
        raycastTransform.transform.parent = gameObject.transform; //So that it moves with the gameObject
        raycastTransform.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y); //Sigue sin ir :)
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
