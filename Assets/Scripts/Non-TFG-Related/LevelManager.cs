using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
