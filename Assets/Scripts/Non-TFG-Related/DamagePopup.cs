using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;                                //TextMeshPro, Visual puede decir que no existe, pero si lo hace.

public class DamagePopup : MonoBehaviour {


    TextMeshPro damageText;                  

    // Use this for initialization
    void OnEnable () {
        damageText = GetComponent<TextMeshPro>();
	}

    public void Setup(int damageAmount)
    {
        damageText.SetText("-" + damageAmount.ToString());
        StartCoroutine(DamageTextAnimation());
    }

    IEnumerator DamageTextAnimation()
    {
        Color originalColor = damageText.color;
        for (float t = 0.01f; t < 1f; t += Time.deltaTime)
        {
            //FadeOut
            damageText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 1f));
            //Subimos el texto
            damageText.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f *Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

}
