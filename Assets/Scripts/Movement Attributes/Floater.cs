using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Floater can float, fly or levitate. 
/// An entity with this behaviour will oscilate between two given points.
/// 
/// TODO: este va a ser Horizontal, dividir en horizontal y vertical para poder hacer el del seno.
/// </summary>
public class Floater : MonoBehaviour {

    public Transform position_one;
    public Transform position_two;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		iTween.MoveUpdate(gameObject, iTween.Hash("y", 4, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
    }
}
