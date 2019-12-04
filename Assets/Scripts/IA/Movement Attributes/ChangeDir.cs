using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Abstract class, which is common to all the movement scripts that imply some change of direction*/
public enum axis { X, Y }
public abstract class ChangeDir : MonoBehaviour
{
    
   protected void ChangeDirection(GameObject gameObject, axis axis)
    {
        if(axis == axis.X)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x * -1, gameObject.transform.position.y); //Nope, no es así.
        }
        else //axis == axis.Y
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y * -1);

        }
    }
}
