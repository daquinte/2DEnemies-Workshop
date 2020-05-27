using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bumper : AbstractChangeDir
{

    public float distanceToTurn = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        SetupDir();

        Renderer rend = GetComponent<Renderer>();
        GetComponent<Rigidbody2D>().freezeRotation = true;

        BoxCollider2D bumperCollider = gameObject.AddComponent<BoxCollider2D>();

        //Offset en Y = 0
        //Offset en X 2 -> -0.1 
        bumperCollider.offset = new Vector3(distanceToTurn*-0.1f/2, 0);

        //Size no es cuadrado
        //Y = 0.25
        //X -> 2 -> 0.4
        bumperCollider.size = new Vector2(distanceToTurn*0.4f/2, 0.25f);
        bumperCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.layer.Equals(GameManager.instance.GetGroundLayer()))
        {
            ChangeDir();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 positionVector = new Vector3(distanceToTurn * -0.1f / 2, transform.position.y);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x -distanceToTurn, transform.position.y));

    }

}
