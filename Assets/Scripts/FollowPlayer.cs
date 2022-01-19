using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    private EnemyController _enemyController;

    public LayerMask characterLayer;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        
        // Follow the player if close to him
        if (Vector2.Distance(transform.position, _player.transform.position) < 7.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemyController.speed * Time.deltaTime);
        }
    }
}
