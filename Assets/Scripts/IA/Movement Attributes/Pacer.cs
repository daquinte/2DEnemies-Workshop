using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// The enemy moves in a straight line, in the direction you specify, 
/// but changes direction in response to a trigger.
/// </summary>
/// 
public class Pacer : AbstractChangeDir { 


    private GameObject raycastEmitter;                  //GameObject you throw the raycast from. Created dinamically.
    private int groundLayerMask;                        //Ground layer for raycast

    // Start is called before the first frame update
    void Start()
    {
        groundLayerMask = (LayerMask.GetMask("Ground"));
        Setup();
    }

    /// <summary>
    /// Placing of the raycast gameObject
    /// </summary>
    void Setup()
    {
        base.SetupDir();

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
        base.MoveForward();
        Check();
    }



    /// <summary>
    /// Checks if there is ground beneath this GameObject. 
    /// If there isn´t, we rotate the gameObject
    /// </summary>
    protected override void Check()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(raycastEmitter.transform.position, Vector2.down, 2, groundLayerMask);
        if (groundRay.collider == null)
        {
            base.ChangeDir();
        }
    }



}
