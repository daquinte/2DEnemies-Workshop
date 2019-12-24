﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerSensor))]         //A Bumper entity needs a sensor to change direction
public class Bumper : AbstractChangeDir
{
    [Range(2.0f, 10.0f)]
    public float colliderWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        Setup();    
    }


    void Setup()
    {
        SetupDir();
        //Place the Transform you cast rays from
        BoxCollider2D bumperCollider = gameObject.AddComponent<BoxCollider2D>();
        bumperCollider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y);
        bumperCollider.size = new Vector2(colliderWidth, bumperCollider.size.y);
        bumperCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aaa");
        ChangeDir();

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //TODO: revisar
       // Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + colliderWidth, transform.position.y));
    }

}
