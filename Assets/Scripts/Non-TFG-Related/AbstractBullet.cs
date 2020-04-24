using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 3;

    protected Rigidbody2D rb;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }


}
