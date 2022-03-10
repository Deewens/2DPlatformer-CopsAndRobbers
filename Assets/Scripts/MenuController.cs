using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{

    public void loadGameplay()
    {
        SceneManager.LoadScene("Level 1");
        //SceneManager.LoadScene("End Scene");

    }

    public void highScores()
    {
        SceneManager.LoadScene("End Scene");
    }

    public void loadStageOption()
    {
        //SceneManager.LoadScene("LevelSelector");
        Debug.Log("Level Selection");
    }


    public void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
        //GameController.instance.sceneIndex = 1;
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene("Level 2");
        //GameController.instance.sceneIndex = 2;
    }
}
