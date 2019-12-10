using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastSensor))]


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
        if (GetComponent<RaycastSensor>().GetSensorActive()) {
            ChangeDir();
        }
    }
}
