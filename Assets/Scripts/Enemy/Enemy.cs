using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy requerirá que el objeto tenga un componente Health
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

    private Health EnemyHealth;

    private void Start()
    {
        EnemyHealth = GetComponent<Health>();
    }
    private void Update()
    {
        //Llamada al modulo de IA. Aun no sé bien como irá esto.
    }

    //Daña a la entidad
    public void Damage(int dmg)
    {
        if(EnemyHealth != null) { 
            EnemyHealth.Damage(dmg);
        }
    }

    public void OnDeath()
    {
        Debug.Log("I just died!");
    }



  
}
