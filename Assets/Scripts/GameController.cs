using System;
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

    [SerializeField] private EnemyController enemyController;
    [SerializeField] private PoliceDroneController policeDroneController;
    private bool _isLoadingSave = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!_isLoadingSave) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
            Destroy(enemy);
        
        GameObject[] policeDrones = GameObject.FindGameObjectsWithTag("Police Drone");
        foreach (var policeDrone in policeDrones)
            Destroy(policeDrone);

        GameData data = SaveSystem.LoadGameData();
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        player.LoadPlayer(data.playerHealth, data.playerPosition);
        updateScore(data.playerScore);

        foreach (var enemyData in data.enemies)
        {
            Vector2 position = new Vector2(enemyData.position[0], enemyData.position[1]);

            var newEnemy = Instantiate(enemyController, position, Quaternion.identity);
            newEnemy.LoadEnemy(enemyData);
        }

        foreach (var droneData in data.policeDrones)
        {
            if (droneData != null)
            {
                Vector2 position = new Vector2(droneData.position[0], droneData.position[1]);

                var newEnemy = Instantiate(policeDroneController, position, Quaternion.identity);
                newEnemy.LoadPoliceDrone(droneData);
            }
        }

        _isLoadingSave = false;
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
        _isLoadingSave = true;
        GameData data = SaveSystem.LoadGameData();
        SceneManager.LoadScene(data.currentSceneIdx, LoadSceneMode.Single);
    }
}
