using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{

    public void loadGameplay()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void loadStageOption()
    {
        //SceneManager.LoadScene("LevelSelector");
        Debug.Log("Level Selection");
    }


    public void loadSetting()
    {
        Debug.Log("Setting");
    }


}
