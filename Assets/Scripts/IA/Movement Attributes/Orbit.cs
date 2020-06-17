using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds the class to its AddComponent field
[AddComponentMenu("EnemiesWorkshop/Movements/Orbit")]

/// <summary>
/// Orbits around a given point in space
/// </summary>
public class Orbit : MonoBehaviour
{
    [Tooltip("Center of the orbit movement")]
    public Vector2 orbitationalCenter;

    [Tooltip("Radius of the orbitational sphere")]
    public float radius = 2.0f;
    [Tooltip("speed of the orbitational movement")]
    public float attractionSpeed = 0.5f;
    [Tooltip("Rotation speed for this entity")]
    public float rotationSpeed = 80.0f;
    [Space(5)]
    [Tooltip("how you want this object to rotate?")]
    public bool rotateClockwise = true;
    [Tooltip("Do you want this object to collide?")]
    public bool collideWithTerrain = false;



    private Vector2 desiredPosition;
    private Vector2 GizmoPosition;

    private bool drawEditorGizmos = true;

    // Start is called before the first frame update
    void Start()
    {
        drawEditorGizmos = false;

        //Set up the orbit´s location
        orbitationalCenter += (Vector2)transform.position;
        GizmoPosition = orbitationalCenter;

        if (collideWithTerrain)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
            }
            rb.gravityScale = 0;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        float mod = (rotateClockwise) ? -180 : 1;
        transform.RotateAround(orbitationalCenter, Vector3.forward * mod, rotationSpeed * Time.deltaTime);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        desiredPosition = (pos - orbitationalCenter).normalized * radius + orbitationalCenter;
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, Time.deltaTime * attractionSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 gizmoPos;
        if (drawEditorGizmos)
        {
            gizmoPos = (Vector2)transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gizmoPos + orbitationalCenter, 0.3f);
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(gizmoPos + orbitationalCenter, radius);
        }
        else
        {
            gizmoPos = GizmoPosition;
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gizmoPos, 0.3f);
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(gizmoPos, radius);
        }
        
    }
}
