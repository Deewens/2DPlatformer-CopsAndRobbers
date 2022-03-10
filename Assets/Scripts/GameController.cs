using System.Collections;
using System.Collections.Generic;
using Player_Related;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    int score = 0;
    public int sceneIndex = 1;
    string sceneName;

    private void Awake()
    {
        instance = this;
    }

    public GameController Instance()
    {
        if (instance == null)
        {
            return instance = this;
        }

        return instance;
    }

    public void changeLevel()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndex++;

        if (sceneIndex < 4)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    void SaveCurrentScene()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    void LoadGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void updateScore(int t_score)
    {
        score += t_score;
        HighScoreScript.instance.currentScore = score;
    }

    public int getScore()
    {
        return score;
    }

    public void SaveGame()
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        GameObject[] enemies;
        List<EnemyController> enemyControllers = new List<EnemyController>();
        
        GameObject[] policeDrones;
        List<PoliceDroneController> policeDroneControllers = new List<PoliceDroneController>();
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
            enemyControllers.Add(enemy.GetComponent<EnemyController>());

        policeDrones = GameObject.FindGameObjectsWithTag("Police Drone");
        foreach (var policeDrone in policeDrones)
        {
            if (policeDrone != null)
                policeDroneControllers.Add(policeDrone.GetComponent<PoliceDroneController>());
        }
        
        SaveSystem.SaveGameData(player, enemyControllers, policeDroneControllers);
    }

    public void LoadSaveGame()
    {
        GameData data = SaveSystem.LoadGameData();
        SceneManager.LoadScene(data.currentSceneIdx, LoadSceneMode.Single);
        Debug.Log("Scene has been reloaded");
        Debug.Log("Player position: {X: " + data.playerPosition[0] + ", Y: " + data.playerPosition[1] + "}");
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        player.LoadPlayer(data.playerHealth, data.playerPosition);
    }
}
