using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2;
    public int health = 100;
    
    public AnimStates animState;
    private static readonly int State = Animator.StringToHash("State");

    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector2 _dir;
    private Vector2 _previousDir;
    private Vector2 _savedLocalState;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _savedLocalState = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        _dir = (Vector2) transform.position - _previousDir;
        _previousDir = transform.position;

        // Change faced direction of the sprite
        if (_dir.x < 0.0) // Move to the right
            transform.localScale = new Vector2(_savedLocalState.x, _savedLocalState.y);

        if (_dir.x > 0.0) // Move to the left
            transform.localScale = new Vector2(-_savedLocalState.x, _savedLocalState.y);

        if (_dir.magnitude > 0.0) animState = AnimStates.Running;
        else animState = AnimStates.Idle;
        
        _animator.SetInteger(State, (int) animState);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
}
