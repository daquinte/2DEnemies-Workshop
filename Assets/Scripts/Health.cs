using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Clase encargada de gestionar la vida de la entidad
/// </summary>
public class Health : MonoBehaviour {

    //Public variables
    public int maxHealth = 1;                       //Vida máxima de la entidad

    //Eventos OnDeath
    [Header("Events")]
    [Space]
    public UnityEvent OnDeathEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth; 
	}

    //Para asignacion posterior al start
    public void Setup(int health, UnityAction callback = null)
    {
        maxHealth = currentHealth = health;
        //TODO: Asignar callbacks al evento 
    }

    //Cura a la entidad 
    public void Heal(int heal)
    {
        currentHealth += heal;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    //Daña a la entidad
    public void Damage(int dmg)
    {
        currentHealth -= dmg;
        //ShowDamage(dmg); //Todo: acumular el daño y mostrarlo cuando deje de recibirlo durante X segundos
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeathEvent.Invoke(); //Invocamos a los callbacks 
        
        ShowDamage(maxHealth);
        Destroy(gameObject);
    }

    //TODO: Gestion de DamageText en un cs aparte??
    void ShowDamage(int damageAmount)
    {
        Vector3 textPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +2);
        DamagePopup dmgtxt = Instantiate(PrefabManager.instance.GetDamagePopUp(), textPosition, Quaternion.identity);
        dmgtxt.Setup(damageAmount);        
    }

   
    
}
