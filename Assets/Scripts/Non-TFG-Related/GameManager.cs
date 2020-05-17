using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

 
    public  LevelManager levelManager;
    private LevelManager levelManagerInstance;

    private void Awake()
    {
       
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this) 
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        levelManagerInstance = Instantiate(levelManager);
    }


    public LevelManager GetLevelManager()
    {
        return (levelManagerInstance != null) ? levelManagerInstance : null;
    }

    //Returns the physics layer for the ground
    public int GetGroundLayer()
    {
        //Layer set up
        int g = LayerMask.GetMask("Ground");
        if (g == 0)
        {
            g = LayerMask.GetMask("ground");
            if (g == 0)
            {
                Debug.LogWarning("[GAME MANAGER WARNING] A Ground layer, set in the platforms, is required for a behaviour to work!");
            }
        }
        return g;
    }

    public int GetPlayerLayer()
    {
        //Layer set up
        int g = LayerMask.GetMask("Player");
        if (g == 0)
        {
            g = LayerMask.GetMask("player");
            if (g == 0)
            {
                Debug.LogWarning("[GAME MANAGER WARNING] A Player layer, set in the Quote game object, is required for a behaviour to work!");
            }
        }
        return g;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Callback cuando cargas una escena.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelManagerInstance = Instantiate(levelManager);
    }
}
