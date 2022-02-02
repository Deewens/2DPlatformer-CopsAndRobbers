using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    private EnemyController _enemyController;

    public LayerMask characterLayer;

    public Transform castPoint;
    public float _agroDistance;

    bool _facingWest = false;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanSeePlayer(_agroDistance))
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemyController.speed * Time.deltaTime);
        }

        //// Follow the player if close to him
        //if (Vector2.Distance(transform.position, _player.transform.position) < 7.5)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemyController.speed * Time.deltaTime);
        //}
    }

    void ChasePlayer()
    {
        if (transform.position.x < _player.transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
            _facingWest = false;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            _facingWest = true;
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool seePlayer = false;
        float castDist = distance;

        if (_facingWest)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));



        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                seePlayer = true;
                //Debug.Log("Player Spotted");
            }
            else
            {
                seePlayer = false;
            }

            Debug.DrawLine(castPoint.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }


        return seePlayer;
    }
}
