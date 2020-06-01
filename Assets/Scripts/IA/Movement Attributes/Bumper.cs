using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Bumper : AbstractChangeDir
{

    public float detectionDistance = 2f;

    private GameObject viewPoint;                  //A position marking where to cast the view box. Created dinamically.


    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        SetupDir();

        GetComponent<Rigidbody2D>().freezeRotation = true;

        viewPoint = new GameObject("BumperViewPoint");
        Renderer rend = GetComponent<Renderer>();
        viewPoint.transform.position = new Vector2(transform.position.x - rend.bounds.extents.x, transform.position.y);
        viewPoint.transform.parent = gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        List<RaycastHit2D> bumperRay = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        Vector2 dir = (pMovementSpeed < 0) ? Vector2.left : Vector2.right;
        int PlayerRayCount = Physics2D.BoxCast(viewPoint.transform.position, new Vector2(detectionDistance,1), 0f, dir, contactFilter2D, bumperRay, detectionDistance);

        if (PlayerRayCount != 0)
        {
            int i = 0;
            bool stop = false;

            while (i < PlayerRayCount && !stop)
            {
                if (bumperRay[i].collider.gameObject.layer != GameManager.instance.GetGroundLayer() && bumperRay[i].collider.gameObject.name != "BaseEnemy") //Cambiar esto :DD:D
                {
                    ChangeDir();
                    stop = true;
                }
                i++;
            }
        }

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
        Gizmos.color = Color.green;
        Renderer rend = GetComponent<Renderer>();
        Vector2 gizmosPos = new Vector2(transform.position.x - rend.bounds.extents.x, transform.position.y);
        Vector2 gizmosDistace = new Vector2(gizmosPos.x - detectionDistance, gizmosPos.y);
        Gizmos.DrawLine(gizmosPos, gizmosDistace);
    }

}
