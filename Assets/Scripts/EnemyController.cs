using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Enums;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
    public float speed = 2;
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;
    
    public BlueguardAnimStates blueguardAnimState;
    private static readonly int AnimState = Animator.StringToHash("State");

    private Rigidbody2D _rb;
    private Animator _animator;
    
    private bool _isFacingRight = true;  // For determining which way the player is currently facing.
    public bool IsFacingRight => _isFacingRight;
    
    private Vector2 _dir;
    private Vector2 _previousDir;

    public bool IsFollowPlayer { set; private get; }

    public int CurrentHealth => _currentHealth;


    void Initialize()
    {
        SaveSystem.enemies.Add(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        Initialize();
    }

    public void LoadEnemy()
    {
        GameData data = SaveSystem.LoadGameData();

        transform.position = new Vector3(data.positionEnemy[0], data.positionEnemy[1], data.positionEnemy[2]);
        _currentHealth = data.healthEnemy;
    }

    // Update is called once per frame
    private void Update()
    {
        /*if (!IsFollowPlayer)
            Patrol();*/

        _dir = ((Vector2) transform.position - _previousDir).normalized;
        _previousDir = transform.position;
        
        if (_dir.x > 0 && !_isFacingRight)
            FlipFacedDirection();
        else if (_dir.x < 0 && _isFacingRight)
            FlipFacedDirection();

        if (_rb.velocity.magnitude > 0 || _dir.x > 0 || _dir.x < 0) blueguardAnimState = BlueguardAnimStates.Running;
        else blueguardAnimState = BlueguardAnimStates.Idle;
        
        _animator.SetInteger(AnimState, (int) blueguardAnimState);
    }

    private void Patrol()
    {
        var offset = 0.5f;
        if (!_isFacingRight) offset = -offset;
        
        var velocity = new Vector2(speed, _rb.velocity.y);

        var startPos = (Vector2) transform.position + (Vector2.right * offset);
        
        Debug.DrawRay(startPos, Vector2.down, Color.yellow);
        var hit = Physics2D.Raycast(startPos, Vector2.down, 5);
        
        Debug.Log(offset);
        Debug.Log(_isFacingRight);
        
        if (hit.collider == null)
        {
            velocity.x = -velocity.x;
        }
        
        _rb.velocity = velocity;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {

            if (GameController.instance != null)
            {
                GameController.instance.updateScore(10);
            }

            _currentHealth = 0;
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Change the faced direction of the player
    /// </summary>
    private void FlipFacedDirection()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
