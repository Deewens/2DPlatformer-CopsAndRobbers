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
        //if (CanSeePlayer(_agroDistance))
        //{
        //    _enemyController.IsFollowPlayer = true;
        //    //transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemyController.speed * Time.deltaTime);
        //}
        //else
        //{
        //    _enemyController.IsFollowPlayer = false;
        //}

        if (CanSeePlayer())
        {
            if (Time.time > _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + fireRate;
            }
        }
    }

    bool CanSeePlayer()
    {
        bool seePlayer = false;

        if (_enemyController.mustTurn)
        {
            shootDistance *= -1;
            shootPoint.localScale = new Vector3(shootPoint.localScale.x * -1, shootPoint.localScale.y * -1, shootPoint.localScale.z);
        }

        Vector2 endPos = castPoint.position + Vector3.right * shootDistance;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                seePlayer = true;
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
        var obj = Instantiate(bullet, shootPoint.transform.position, bullet.transform.rotation);

        if (_enemyController.transform.localScale.x == -1)
        {
            obj.transform.right = -obj.transform.right;
        }
    }
}