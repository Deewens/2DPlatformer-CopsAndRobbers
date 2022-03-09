using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string scene;
    public int health;
    public int healthEnemy;
    public float[] positionPlayer = new float[3];
    public float[] positionEnemy = new float[3];
    public float[] positionCamera = new float[3];

    public GameData(PlayerController player, EnemyController enemy)
    {
        //level = gameController;
        health = player.CurrentHealth;
        positionPlayer[0] = player.transform.position.x;
        positionPlayer[1] = player.transform.position.y;
        positionPlayer[2] = player.transform.position.z;

        healthEnemy = enemy.CurrentHealth;
        positionEnemy[0] = enemy.transform.position.x;
        positionEnemy[1] = enemy.transform.position.y;
        positionEnemy[2] = enemy.transform.position.z;

        positionCamera[0] = Camera.main.transform.position.x;
        positionCamera[1] = Camera.main.transform.position.y;
        positionCamera[2] = Camera.main.transform.position.z;

    }
}
