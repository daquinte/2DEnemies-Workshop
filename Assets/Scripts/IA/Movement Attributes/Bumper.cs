using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : ChangeDir
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection(this.gameObject, axis.X);
    }
}
