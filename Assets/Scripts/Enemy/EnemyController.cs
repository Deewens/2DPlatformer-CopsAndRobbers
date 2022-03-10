using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Enums;
using Save_System;
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
    public Transform groundCheckPos;
    public bool mustTurn;

    private Rigidbody2D _rb;
    private Animator _animator;
   

    public bool IsFollowPlayer { set; private get; }
    public bool mustPatrol;


    public int CurrentHealth => _currentHealth;

    public LayerMask groundLayer;

    public Collider2D wallCollider;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        mustPatrol = true;
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    public void LoadEnemy(EnemyData enemyData)
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (mustPatrol)
        Patrol();

        if (_rb.velocity.magnitude > 0 && mustPatrol) blueguardAnimState = BlueguardAnimStates.Running;
        else blueguardAnimState = BlueguardAnimStates.Idle;
        
        _animator.SetInteger(AnimState, (int) blueguardAnimState);
    }

    private void Patrol()
    {

        if (mustTurn || wallCollider.IsTouchingLayers(groundLayer))
        {
            FlipFacedDirection();
        }
        
        _rb.velocity = new Vector2(speed * Time.fixedDeltaTime, _rb.velocity.y);
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
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
    }
}
