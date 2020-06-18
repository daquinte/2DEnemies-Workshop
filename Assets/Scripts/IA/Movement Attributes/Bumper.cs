using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Bumper")]

//A Bumper enemy advances in a straight line, but changes direction when it encounters a gameobject
public class Bumper : AbstractChangeDir
{

    public float detectionDistance = 2f;

    private GameObject viewPoint;                                       //A position marking where to cast the view box. Created dinamically.



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
        EnemyEngine enemyEngine = GetComponent<EnemyEngine>();
        List<RaycastHit2D> bumperRay = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        Vector2 dir = (pMovementSpeed < 0) ? Vector2.left : Vector2.right;
        int PlayerRayCount = Physics2D.BoxCast(viewPoint.transform.position, new Vector2(detectionDistance, 0.5f), 0f, dir, contactFilter2D, bumperRay, detectionDistance);

        int i = 0;
        bool stop = false;

        while (i < PlayerRayCount && !stop)
        {
            if (bumperRay[i].collider.gameObject.layer != enemyEngine.GetGroundLayer() && bumperRay[i].collider.gameObject.name != "BaseEnemy") //TODO: Cambiar esto :DD:D
            {
                ChangeDir();
                Renderer rend = GetComponent<Renderer>();
                if (pMovementDir < 0)
                {
                    viewPoint.transform.position = new Vector2(transform.position.x + rend.bounds.extents.x, transform.position.y);
                }
                else
                {
                    viewPoint.transform.position = new Vector2(transform.position.x - rend.bounds.extents.x, transform.position.y);
                }
                stop = true;
            }
            i++;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.layer.Equals(GetComponent<EnemyEngine>().GetGroundLayer()))
        {
            ChangeDir();

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Renderer rend = GetComponent<Renderer>();
        Vector2 gizmosPos;
        Vector2 gizmosDistace;
        if (transform.rotation.y == 0)
        {
            gizmosPos = new Vector2(transform.position.x - rend.bounds.extents.x, transform.position.y);
            gizmosDistace = new Vector2(gizmosPos.x - detectionDistance, gizmosPos.y);
        }
        else
        {
            gizmosPos = new Vector2(transform.position.x + rend.bounds.extents.x, transform.position.y);
            gizmosDistace = new Vector2(gizmosPos.x + detectionDistance, gizmosPos.y);
        }
        Gizmos.DrawLine(gizmosPos, gizmosDistace);
    }

}
