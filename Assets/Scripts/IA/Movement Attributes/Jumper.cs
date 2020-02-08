using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need a Rigidbody in order to apply force to the gameObject
[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// Jumps as high ad the "jump force" variable.
/// You can especify the delay between jumps, as well as the jump force. 
/// 
/// USAGE:
/// Combined with a Liner, a Jumper enemy will bounce towards another gameObject, usually the Player.
/// Add an instance of ForwardJumper if you want that behaviour
/// </summary>
public class Jumper : MonoBehaviour
{
    //TODO: Rework de Jumper -> Necesito un punto maximo, y el tiempo que quiero alcanzarlo. Con eso calculo la fuerza que le tengo que meter al rb
    //Public attributes
    public float jumpHeight;                            //How high you want this entity to jump


    /// <summary>
    /// Apply an impulse force to the GameObject to make it jump
    /// </summary>
    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight * 1.5f), ForceMode2D.Impulse);
        //En lugar de hacer aqui el addforce creo que lo mejor sería pasarle el valor al engine, el new vector 2 y que sólo se aplique una vez
    }

}
