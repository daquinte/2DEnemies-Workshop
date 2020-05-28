using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [Tooltip("Center of the orbit movement")]
    public Vector2 orbitationalCenter;

    [Tooltip("Radius of the orbitational sphere")]
    public float radius = 2.0f;
    [Tooltip("speed of the orbitational movement")]
    public float radiusSpeed = 0.5f;
    /*[Tooltip("Do you want this object to rotate?")]
    public bool rotateWithOrbit;*/
    [Tooltip("Rotation speed for this entity")]
    public float rotationSpeed = 80.0f;

    private Vector2 desiredPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        transform.RotateAround(orbitationalCenter, Vector3.forward * -180, rotationSpeed * Time.deltaTime);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        desiredPosition = (pos - orbitationalCenter).normalized * radius + orbitationalCenter;
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(orbitationalCenter, 0.3f);
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(orbitationalCenter, radius);
    }
}
