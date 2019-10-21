using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 3;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerManager h = hitInfo.GetComponent<PlayerManager>();
        if (h != null)
        {
            h.DamagePlayer(damage);
            Destroy(this.gameObject);
        }
       
    }
}
