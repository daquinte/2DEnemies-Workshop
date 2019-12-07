using System.Collections;
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
        //Set the direction (BASE)
        float dir = 0f;

        if(transform.eulerAngles.y == 0) { 
             dir = (initialMovement == InitialMovement.Left) ? -1f : 1f;
        }
        else if (transform.eulerAngles.y == 180)
        {
             dir = (initialMovement == InitialMovement.Left) ? 1f : -1f;

        }
        movementSpeed *= dir;


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
        Move();
        Check();
    }

    /// <summary>
    /// Moves the entity, always to the right but modified by the movementSpeed´s
    /// magnitude and it being positive or negative.
    /// </summary>
    protected void Move()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if there is ground beneath. 
    /// If there isn´t, we rotate the gameObject
    /// </summary>
    protected virtual void Check()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(raycastEmitter.transform.position, Vector2.down, 2, groundLayerMask);
        if (groundRay.collider == null)
        {
            ChangeDir();
        }
    }

    protected void ChangeDir()
    {
        transform.Rotate(Vector2.up, 180);

    }

}
