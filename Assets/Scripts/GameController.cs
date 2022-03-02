using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController instance;
        
    string sceneName;

    private void Awake()
    {
        instance = this;
    }

    void SaveCurrentScene()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    void LoadGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
