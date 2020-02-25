using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy Bullet that deals damage to the player
/// </summary>
public class EnemyBullet : AbstractBullet
{
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
