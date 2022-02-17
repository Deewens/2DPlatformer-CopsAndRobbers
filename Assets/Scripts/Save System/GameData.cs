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

    public GameData(PlayerController player, EnemyController enemy)
    {
        //level = gameController;
        health = player._health;
        positionPlayer[0] = player.transform.position.x;
        positionPlayer[1] = player.transform.position.y;
        positionPlayer[2] = player.transform.position.z;

        healthEnemy = enemy.health;
        positionEnemy[0] = enemy.transform.position.x;
        positionEnemy[1] = enemy.transform.position.y;
        positionEnemy[2] = enemy.transform.position.z;

    }
}
