using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used as a "game simulation" class
/// It is created as to make believe that this is a level manager, and thus is has the current player´s instance
/// </summary>
public class LevelManager : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPlayer()
    {
        return (player != null) ? player : null;
    }
}
