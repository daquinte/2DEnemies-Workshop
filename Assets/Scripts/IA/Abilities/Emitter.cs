using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Emitter : MonoBehaviour
{
    public GameObject proyectile;       //The object that will be emitted

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShootTest");
    }
    
    // Prueba para las balas. La condición de disparo será por posicion del jugador
    IEnumerator ShootTest()
    {
        while (true) {
            yield return new WaitForSecondsRealtime(1f);
            Vector3 rotation = (transform.eulerAngles.y == 0)
                ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
            Instantiate(proyectile, transform.position, Quaternion.Euler(rotation));
            yield return null;

        }
    }
}
