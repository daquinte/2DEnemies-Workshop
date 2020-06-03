using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// The enemy moves in a straight line, in the direction you specify, 
/// but changes direction in response to a trigger.
/// </summary>
/// 
public class Pacer : AbstractChangeDir
{


    private GameObject raycastEmitter;          //GameObject you throw the raycast from. Created dinamically.

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    /// <summary>
    /// Placing of the raycast gameObject
    /// </summary>
    void Setup()
    {
        SetupDir();

        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().freezeRotation = true;

        //Place the Transform you cast rays from
        raycastEmitter = new GameObject("raycastEmitter");

        Renderer rend = GetComponent<Renderer>();
        raycastEmitter.transform.position = new Vector3(transform.position.x - rend.bounds.extents.x, transform.position.y);
        raycastEmitter.transform.parent = gameObject.transform;

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
        List<RaycastHit2D> rayCastInfo = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        int sensorRay = Physics2D.Raycast(raycastEmitter.transform.position, Vector2.down, contactFilter2D, rayCastInfo, 1);
        Debug.DrawRay(raycastEmitter.transform.position, Vector2.down, Color.green);


        int i = 0;
        bool floorFound = false;

        while (i < sensorRay)
        {
            if (rayCastInfo[i].collider.gameObject.layer == GameManager.instance.GetGroundLayer())
            {
                floorFound = true;
            }
            i++;
        }

        //If my ray did NOT detect any floor, we change dir
        if (!floorFound) ChangeDir();
    }
}

