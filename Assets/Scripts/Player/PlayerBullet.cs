using UnityEngine;
using System.Collections;

public class PlayerBullet : AbstractBullet
{
    public float timeToLive = 2;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
        StartCoroutine("TimeToLive");
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy h = hitInfo.GetComponent<Enemy>();
        if (h != null)
        {
            h.Damage(damage);
        }
        Destroy(this.gameObject);
    }

    IEnumerator TimeToLive()
    {
        while (timeToLive > 0)
        {
            yield return new WaitForSeconds(0.5f);
            timeToLive--;
        }
        if (gameObject.activeInHierarchy)
            Destroy(gameObject);
    }

}
