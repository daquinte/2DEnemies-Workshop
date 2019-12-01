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
