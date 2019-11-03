using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Floater can float, fly or levitate. 
/// An entity with this behaviour will oscilate a given distance.
/// You should place the entity in the *Lowest* point of the oscilation.
/// </summary>
/// 
public enum MovementAxis{ //Axis in which a Floater can move
    X,
    Y
};

public class Floater : MonoBehaviour {

    public float movementDistance = 4f;              //Total distance you want to cover in unity units
    public float movementSpeed = 1f;                 //Unity units per second in which the enemy will move
    public float delayTime = 0.1f;                   //Delay in Realtime Seconds the entity will wait at the ends of each path
    [SerializeField] private MovementAxis axis = MovementAxis.Y;       // Axis you want to float on

    //Private movement attributes

    private float upperLimit;
    private float lowerLimit;
    private float current;

    
    private bool towardsUpperLimit;

    
    // Use this for initialization
    void Start () {
        towardsUpperLimit = true;
        Setup();
        //iTween.MoveBy(gameObject, iTween.Hash(StrAxis, movementDistance, "easeType", "easeInOutSine", "loopType", "pingPong", "delay", delayTime));
        if(axis == MovementAxis.Y) { 
            StartCoroutine("FloatMovementInYAxis");
        }
        else if (axis == MovementAxis.X)
        {
            StartCoroutine("FloatMovementInXAxis");
        }
    }

    void Setup()
    {
        if (axis == MovementAxis.Y) {
            //We set the limits for Y
            upperLimit = transform.position.y + (movementDistance / 2f);
            lowerLimit = transform.position.y - (movementDistance / 2f);
            current = transform.position.y;
        }
        else if(axis == MovementAxis.X)
        {
            //We set the limits for X
            upperLimit = transform.position.x + (movementDistance / 2f);
            lowerLimit = transform.position.x - (movementDistance / 2f);
            current = transform.position.x;
        }
    }


    /// <summary>
    /// Called then the component is disabled.
    /// We stop all coroutines, either the X or Y axis corroutine will be stopped.
    /// </summary>
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //TODO: Unir FloatMovement in X/Y Axis de alguna manera
    //TODO: Quitar coroutinas

    IEnumerator FloatMovementInYAxis()
    {
        while (true) { 
            if (towardsUpperLimit)
            {
                if (transform.position.y != upperLimit)
                {
                    current = Mathf.MoveTowards(current, upperLimit, movementSpeed * Time.deltaTime);
                }
                else { 
                    towardsUpperLimit = false;
                    yield return new WaitForSecondsRealtime(delayTime);
                }
            }
            //Then you´re going for rhe lower
            else if (!towardsUpperLimit)
            {
                if (transform.position.y != lowerLimit)
                {
                    current = Mathf.MoveTowards(current, lowerLimit, movementSpeed * Time.deltaTime);
                }
                else { 
                    towardsUpperLimit = true;
                    yield return new WaitForSecondsRealtime(delayTime);
                }
            }

            transform.position = new Vector3(transform.position.x, current);
            yield return null;
        }

    }

    IEnumerator FloatMovementInXAxis()
    {
        while (true)
        {
            if (towardsUpperLimit)
            {
                if (transform.position.x != upperLimit)
                {
                    current = Mathf.MoveTowards(current, upperLimit, movementSpeed * Time.deltaTime);
                }
                else
                {
                    towardsUpperLimit = false;
                    yield return new WaitForSecondsRealtime(delayTime);
                }
            }
            //Then you´re going for rhe lower
            else if (!towardsUpperLimit)
            {
                if (transform.position.x != lowerLimit)
                {
                    current = Mathf.MoveTowards(current, lowerLimit, movementSpeed * Time.deltaTime);
                }
                else
                {
                    towardsUpperLimit = true;
                    yield return new WaitForSecondsRealtime(delayTime);
                }
            }

            transform.position = new Vector3(current, transform.position.y);
            yield return null;
        }

    }


    /// <summary>
    /// Gizmos for the Floater behaviour
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        switch (axis)
        {
            case MovementAxis.X:
                float posAuxX = transform.position.x - movementDistance / 2;
                Gizmos.DrawLine(transform.position, new Vector3(posAuxX + movementDistance, transform.position.y));    
                break;
            case MovementAxis.Y:
                float posAuxY = transform.position.y - movementDistance / 2;
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, posAuxY + movementDistance));
                break;
        }
      
    }

}
