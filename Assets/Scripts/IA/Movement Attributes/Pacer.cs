using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// The enemy moves in a straight line, in the direction you specify, 
/// but changes direction in response to a trigger.
/// </summary>
/// 
public class Pacer : AbstractChangeDir {

    
    private LayerMask m_WhatIsGround;           //A mask determining what is ground for this entity

    private GameObject raycastEmitter;          //GameObject you throw the raycast from. Created dinamically.

    // Start is called before the first frame update
    void Start()
    {
        m_WhatIsGround = GameManager.instance.GetGroundLayer();

        Setup();     
    }

    /// <summary>
    /// Placing of the raycast gameObject
    /// </summary>
    void Setup()
    {
        SetupDir();

        //Place the Transform you cast rays from
        raycastEmitter = new GameObject("raycastEmitter");

        Renderer rend = GetComponent<Renderer>();
        raycastEmitter.transform.position = new Vector3(transform.position.x - rend.bounds.extents.x, raycastEmitter.transform.position.y);
        raycastEmitter.transform.parent   = gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        Check();
    }

    /// <summary>
    /// Checks if there is ground beneath this GameObject. 
    /// If there isn´t, we rotate the gameObject
    /// </summary>
    protected override void Check()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(raycastEmitter.transform.position, Vector2.down, 2, m_WhatIsGround);
        if (groundRay.collider == null)
        {
            ChangeDir();
        }
    }
}
