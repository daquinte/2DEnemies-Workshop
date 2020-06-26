using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This component is just stationary, and can not move or be moved
public class Stationary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //We sleep the rigidbody
       Rigidbody2D rb =  GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.Sleep();        
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
