using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;

public class PoliceDroneController : MonoBehaviour
{
    [SerializeField] private float agroDistance = 2;
    [SerializeField] private Transform droneGfx;
    
    [SerializeField] private Transform shootPoint;
    [SerializeField] private DroneMissileController missile;

    [SerializeField] private PoliceDroneAnimStates animState;
    private static readonly int State = Animator.StringToHash("State");

    public AudioSource sound;

    private AIPath _aiPath;
    private Animator _animator;
    private GameObject _player;

    private bool _isFacingRight = true;  // For determining which way the player is currently facing.

    private Vector2 _dir;
    private Vector2 _previousDir;

    [SerializeField] private float fireRate = 1;
    private float _nextShootTime;
    
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;
    
    public int CurrentHealth => _currentHealth;


    private void Start()
    {
        _aiPath = GetComponent<AIPath>();
        _animator = GetComponentInChildren<Animator>();
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        _dir = ((Vector2) transform.position - _previousDir).normalized;
        _previousDir = transform.position;

        if (_dir.x > 0.01f && !_isFacingRight)
            FlipFacedDirection();
        else if (_dir.x < -0.01f && _isFacingRight)
            FlipFacedDirection();

        if (_aiPath.velocity.magnitude > 0) animState = PoliceDroneAnimStates.Forward;
        else animState = PoliceDroneAnimStates.Idle;
        
        _animator.SetInteger(State, (int) animState);

        // If the player is close enough to the drone, then enable drone movement
        if (Vector2.Distance(transform.position, _player.transform.position) <= agroDistance)
            _aiPath.isStopped = false;
        else
            _aiPath.isStopped = true;

        Vector2 raycastDir = (_player.transform.position - shootPoint.position).normalized;
        Debug.DrawRay(shootPoint.position, raycastDir * 10, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, raycastDir, agroDistance, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            if (Time.time > _nextShootTime)
            {
                Shoot(raycastDir);
                _nextShootTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot(Vector2 direction)
    {
        var missileObj = Instantiate(missile, shootPoint.transform.position, Quaternion.identity);
        missileObj.LaunchMissile(direction);
        sound.Play();
    }

    /// <summary>
    /// Change the faced direction of the player
    /// </summary>
    private void FlipFacedDirection()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void RemoveHealth(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            GameController.instance.updateScore(10);
            Destroy(gameObject);
        }
    }
}
