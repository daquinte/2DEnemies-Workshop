using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;                                //TextMeshPro, Visual puede decir que no existe, pero si lo hace.

/// <summary>
/// Clase encargada de gestionar la vida de la entidad
/// </summary>
public class Health : MonoBehaviour {

    //Public variables
    public int maxHealth = 1;                       //Vida máxima de la entidad
    public TextMeshPro damageText;                  //Prefab del texto de daño

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
    }

    //Daña a la entidad
    public void Damage(int dmg)
    {
        currentHealth -= dmg;
        ShowDamage(dmg);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeathEvent.Invoke(); //Invocamos a los callbacks 
        // TODO: texto flotante -dmg
        Destroy(gameObject);
    }

    //TODO: Gestion de DamageText en un cs aparte??
    void ShowDamage(int damageAmount)
    {
        Vector3 textPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 100);
        TextMeshPro dmgtxt = Instantiate(damageText, textPosition, Quaternion.identity);
        dmgtxt.SetText("-" + damageAmount.ToString());
        //iTween.FadeTo(test.gameObject, iTween.Hash("alpha", 0.0f, "time", .3)); //No funciona con TextMeshPro
        StartCoroutine(DamageTextAnimation(dmgtxt));
    }

    IEnumerator DamageTextAnimation(TextMeshPro textToFade)
    {
        Color originalColor = textToFade.color;
        for (float t = 0.01f; t < 1f; t += Time.deltaTime)
        {
            //FadeOut
            textToFade.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 1f));
            //Subimos el texto
            textToFade.transform.position = new Vector3(transform.position.x, transform.position.y + 1f);
            yield return null;
        }
     
        Destroy(textToFade.gameObject);
    }

    
}
