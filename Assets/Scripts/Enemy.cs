using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy requerirá que el objeto tenga un componente Health
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

    private void Update()
    {
        //Llamada al modulo de IA. Aun no sé bien como irá esto.
    }
    public void OnDeath()
    {
        Debug.Log("F");
    }
}
