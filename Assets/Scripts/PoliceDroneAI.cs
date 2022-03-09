using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Pathfinding;

public class PoliceDroneAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 200;
    [SerializeField] private float nextWaypointDistance = 3;
    [SerializeField] private float endReachedDistance;

    [SerializeField] private Transform droneGFX;
    
    [SerializeField] private PoliceDroneAnimStates animState;
    private static readonly int State = Animator.StringToHash("State");
    
    private Path _path;
    private int _currentWaypoint;
    private bool _reachedEndOfPath = false;
    
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private bool _isFacingRight = true;  // For determining which way the player is currently facing.

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_path == null) return;

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
            _reachedEndOfPath = false;

        if (Vector2.Distance(target.position, transform.position) <= endReachedDistance)
            _reachedEndOfPath = true;

        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        _rb.AddForce(force);
        
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance)
            _currentWaypoint++;
        
        if (force.x >= 0.01f && !_isFacingRight)
            FlipFacedDirection();
        else if (force.x <= -0.01f && _isFacingRight)
            FlipFacedDirection();

        if (force.magnitude > 0) animState = PoliceDroneAnimStates.Forward;
        else animState = PoliceDroneAnimStates.Idle;
        
        _animator.SetInteger(State, (int) animState);

    }
    
    /// <summary>
    /// Change the faced direction of the player
    /// </summary>
    private void FlipFacedDirection()
    {
        _isFacingRight = !_isFacingRight;
        droneGFX.Rotate(0f, 180f, 0f);
    }
}
