using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;

    public AnimStates animState;
    private static readonly int State = Animator.StringToHash("State");

    public float horizontalsSpeed;
    public float jumpVerticalPushOff;

    private float _horizontalInput;

    Vector2 _savedlocalScale;

    public LayerMask groundLayer;

    public TextMeshProUGUI stateDebugText;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _savedlocalScale = transform.localScale;
    }

    private void Update()
    {
        // what do the below lines do ?
        // They are changing the faced direction of the cat
        if (_horizontalInput > 0.001f)
            transform.localScale = new Vector2(_savedlocalScale.x, _savedlocalScale.y);
        else if (_horizontalInput < -0.001f)
            transform.localScale = new Vector2(-_savedlocalScale.x, _savedlocalScale.y);

        switch (animState)
        {
            case AnimStates.Running:
                if (_horizontalInput == 0) animState = AnimStates.Idle;
                break;
            case AnimStates.Jumping:
                if (_rb.velocity.y < 0)
                    if (IsGrounded())
                    {
                        if (_horizontalInput > 0.001f || _horizontalInput < -0.001f) animState = AnimStates.Running;
                        else animState = AnimStates.Idle;
                    }
                break;
        }

        //_rb.AddForce(new Vector2(_horizontalInput * horizontalsSpeed, _rb.velocity.y), ForceMode2D.Impulse);
        _rb.velocity = new Vector2(_horizontalInput * horizontalsSpeed, _rb.velocity.y);
        _animator.SetInteger(State, (int)animState);
        
        /*if (IsGrounded()) stateDebugText.SetText("Grounded");
        else stateDebugText.SetText("Not grounded");*/
    }

    /*
     * Input events handling
    */
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _horizontalInput = movementVector.x;
        animState = AnimStates.Running;
    }

    private void OnJump()
    {
        if (IsGrounded())
        {
            animState = AnimStates.Jumping;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpVerticalPushOff);
        }
    }

    /// <summary>
    /// Raycast method to check if a game object is grounded
    /// </summary>
    /// <returns>boolean indicating if the player is grounded</returns>
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}