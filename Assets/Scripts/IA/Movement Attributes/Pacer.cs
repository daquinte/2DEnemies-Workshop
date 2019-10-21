﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enumerator to calcule the direction of the entity
public enum InitialMovement { Right, Left };

/// <summary>
/// The enemy moves in a straight line, in the direction you specify, 
/// but changes direction in response to a trigger.
/// </summary>
/// 
public class Pacer : MonoBehaviour { 

    public float movementSpeed = 0.1f;   
    public InitialMovement initialMovement;

    private GameObject raycastEmitter;                  //GameObject you throw the raycast from. Created dinamically.
    private int groundLayerMask;                        //Ground layer for raycast

    // Start is called before the first frame update
    void Start()
    {
        groundLayerMask = (LayerMask.GetMask("Ground"));
        Setup();
    }

    /// <summary>
    /// Setup of the direcion, dependant on he initial movement.
    /// Placing of the raycast gameObject
    /// </summary>
    void Setup()
    {
        //Set the direction
        float dir = 0f;
        Debug.Log(transform.eulerAngles);
        if(transform.eulerAngles.y == 0) { 
             dir = (initialMovement == InitialMovement.Left) ? -1f : 1f;
        }
        else if (transform.eulerAngles.y == 180)
        {
             dir = (initialMovement == InitialMovement.Left) ? 1f : -1f;

        }
        movementSpeed *= dir;
        Debug.Log(movementSpeed);


        //Place the Transform you cast rays from
        Transform GOTransform = transform;
        raycastEmitter = new GameObject("raycastEmitter");
        raycastEmitter.transform.parent = gameObject.transform;
        switch (initialMovement)
        {
            case InitialMovement.Left:
                raycastEmitter.transform.position = 
                    new Vector3(GOTransform.position.x - 1.5f, GOTransform.position.y );
                break;

            case InitialMovement.Right:
                
                raycastEmitter.transform.position = 
                    new Vector3(GOTransform.position.x + 1.5f, GOTransform.position.y - 0.3f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        RaycastHit2D groundRay = Physics2D.Raycast(raycastEmitter.transform.position, Vector2.down, 2, groundLayerMask);
        if(groundRay.collider == null){
            transform.Rotate(Vector2.up, 180);
        }
    }

}
