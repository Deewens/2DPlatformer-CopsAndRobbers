using System.Collections;
using System.Collections.Generic;
using Player_Related;
using Save_System;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int currentSceneIdx;
    public int playerHealth;
    public int playerScore;
    public float[] playerPosition;

    public EnemyData[] enemies;
    public PoliceDroneData[] policeDrones;

    public GameData(PlayerController player, List<EnemyController> enemies, List<PoliceDroneController> policeDrones)
    {
        currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        
        playerHealth = player.CurrentHealth;
        playerScore = GameController.instance.getScore();
        
        playerPosition = new float[2];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;

        this.enemies = new EnemyData[enemies.Count];
        this.policeDrones = new PoliceDroneData[policeDrones.Count];

        for (int i = 0; i < this.enemies.Length; i++)
        {
            EnemyData enemyData = new EnemyData(enemies[i]);
            this.enemies[i] = enemyData;
        }
        
        for (int i = 0; i < this.policeDrones.Length; i++)
        {
            if (policeDrones[i] != null)
            {
                PoliceDroneData policeDroneData = new PoliceDroneData(policeDrones[i]);
                this.policeDrones[i] = policeDroneData;
            }
        }
    }
}
