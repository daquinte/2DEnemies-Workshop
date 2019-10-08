using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy requerirá que el objeto tenga un componente Health
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

    public int contactDamage = 1;

    private void Update()
    {
        //Llamada al modulo de IA. Aun no sé bien como irá esto.
    }

    public void OnDeath()
    {
        Debug.Log("Awesome! I just died!");
    }


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
