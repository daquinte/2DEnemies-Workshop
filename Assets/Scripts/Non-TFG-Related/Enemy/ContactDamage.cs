using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ContactDamage : MonoBehaviour
{
    public int contactDamage = 1;
   
    //Todos los enemigos hacen daño por contacto
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        PlayerManager pm = collisionInfo.gameObject.GetComponent<PlayerManager>();
        if(pm != null)
        {
            pm.DamagePlayer(contactDamage);
        }
    }
}
