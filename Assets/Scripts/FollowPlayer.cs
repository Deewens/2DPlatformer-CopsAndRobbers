using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    private EnemyController _enemyController;
    
    [SerializeField] private float shootDistance = 4f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float fireRate = 1;
    private float _nextShootTime;
    
    public LayerMask characterLayer;

    public Transform castPoint;
    public float _agroDistance;

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
            _enemyController.IsFollowPlayer = true;
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemyController.speed * Time.deltaTime);
        }
        else
        {
            _enemyController.IsFollowPlayer = false;
        }

        if (CanSeePlayer(shootDistance))
        {
            if (Time.time > _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + fireRate;
            }
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool seePlayer = false;
        float castDist = distance;

        if (!_enemyController.IsFacingRight)
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
    
    private void Shoot()
    {
        var obj = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
        if (!_enemyController.IsFacingRight)
        {
            obj.transform.right = -obj.transform.right;
        }
    }
}
