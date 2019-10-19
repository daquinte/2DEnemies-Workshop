﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Floater can float, fly or levitate. 
/// An entity with this behaviour will oscilate a given distance.
/// You should place the entity in the *Lowest* point of the oscilation.
/// </summary>
/// 
public enum MovementAxis{ //Axis in which a Floater can move
    X,
    Y
};

public class Floater : MonoBehaviour {

    public float movementDistance = 4f;              //How long you want the enemy to move upwards from the start position
    public float delayTime = .1f;                    //Time that the entity spends at the edges of the movement
    public MovementAxis axis = MovementAxis.Y;       // Axis you want to float on

    private string StrAxis;                          //String for iTween
    private Vector3 gizmoFirstPos;  

    // Use this for initialization
    void Start () {

        Setup();
        iTween.MoveBy(gameObject, iTween.Hash(StrAxis, movementDistance, "easeType", "easeInOutSine", "loopType", "pingPong", "delay", delayTime));
        
    }

    void Setup()
    {
        StrAxis = (axis == MovementAxis.Y) ? "Y" : "X";
        gizmoFirstPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(gizmoFirstPos, new Vector3(gizmoFirstPos.x, gizmoFirstPos.y + movementDistance));
    }

}
