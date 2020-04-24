using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Clase encargada de gestionar la vida de la entidad
/// </summary>
public class Health : MonoBehaviour {

    //Public variables
    public int maxHealth = 1;                       //Entity´s max health. Better to be changed in the editor.

    //OnDeath events
    private DamagePopup DamagePopup;

    [Header("Events")]
    [Space]
    public UnityEvent OnDeathEvent;     //TODO: ¿Lo uso o no?
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth; 
	}

    //In case you want to start this component dinamically
    public void Setup(int health, UnityAction callback = null)
    {
        maxHealth = currentHealth = health;
        //TODO: Asignar callbacks al evento 
    }

    //Heals the entity. A entity cannot overheal.
    public void Heal(int heal)
    {
        currentHealth += heal;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    //Damages the entity.
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
        OnDeathEvent.Invoke();
        
        ShowDamage(maxHealth);
        Destroy(gameObject);
    }

    /// <summary>
    /// Shows a floating damageText
    /// </summary>
    /// <param name="damageAmount">Damage you want to show</param>
    void ShowDamage(int damageAmount)
    {
        Vector3 textPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +2);
        DamagePopup dmgtxt = Instantiate(DamagePopup, textPosition, Quaternion.identity);
        dmgtxt.Setup(damageAmount);        
    }

   
    
}
