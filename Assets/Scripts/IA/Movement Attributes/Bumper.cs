﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : AbstractChangeDir
{
    [Range(1.0f, 5.0f)]
    public float colliderWidth = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        Setup();    
    }


    void Setup()
    {
        SetupDir();

        Renderer rend = GetComponent<Renderer>();
        BoxCollider2D bumperCollider = gameObject.AddComponent<BoxCollider2D>();
        //La equivalencia base es 3 ancho -> 0.6
        //Entonce 3 ancho -> 0.6 
        //        cw      -> X
        bumperCollider.size = new Vector2(colliderWidth*0.6f/3, colliderWidth * 0.6f / 3);
        bumperCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeDir();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(colliderWidth, colliderWidth));
    }

}
