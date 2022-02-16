using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    string sceneName;
    void LoadGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
