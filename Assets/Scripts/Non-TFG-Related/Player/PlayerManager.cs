using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Clase pensada para que contenga el estado actual del jugador
  Como las colisiones, guardar referencia al arma principal, modificar el firingpoint...*/
[RequireComponent(typeof(Health))]
public class PlayerManager : MonoBehaviour {

    private Health playerHealth;

	// Use this for initialization
	void Start () {
        playerHealth = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamagePlayer(int dmg)
    {
        playerHealth.Damage(dmg);
    }

    public void OnPlayerDeath()
    {
        Debug.Log("JUGADOR MUERTO");
    }
}
