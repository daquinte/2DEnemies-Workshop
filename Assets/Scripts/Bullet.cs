using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 3;
    public float timeToLive = 2;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine("TimeToLive");
	}

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Health h = hitInfo.GetComponent<Health>();
        if(h != null)
        {
            h.Damage(damage);
        }
        Destroy(this.gameObject);
    }

    IEnumerator TimeToLive()
    {
        while (timeToLive > 0) { 
        yield return new WaitForSeconds(0.5f);
            timeToLive--;
        }
        if(gameObject.activeInHierarchy)
            Destroy(gameObject);
    }

}
