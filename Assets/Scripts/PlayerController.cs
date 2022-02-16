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
    private CapsuleCollider2D _capsuleCollider;
    
    private bool _isFacingRight = true;  // For determining which way the player is currently facing.

    public AnimStates animState;
    private static readonly int State = Animator.StringToHash("State");
    private static readonly int IsArmed = Animator.StringToHash("isArmed");

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
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        _animator.SetBool(IsArmed, true);

        _savedlocalScale = transform.localScale;
    }

    private void Update()
    {
        if (_horizontalInput > 0.001f && !_isFacingRight)
            FlipFacedDirection();
        else if (_horizontalInput < -0.001f && _isFacingRight)
            FlipFacedDirection();

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
        
        if (IsGrounded()) stateDebugText.SetText("Grounded");
        else stateDebugText.SetText("Not grounded");
    }

    /*
     * Input events handling
    */
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _horizontalInput = movementVector.x;
        if (IsGrounded()) animState = AnimStates.Running;
    }

    public void Moving(Vector2 movement)
    {
        _horizontalInput = movement.x;
        if (IsGrounded()) animState = AnimStates.Running;
    }

    public void OnJump()
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(_capsuleCollider.bounds.center, _capsuleCollider.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    /// <summary>
    /// Change the faced direction of the player
    /// </summary>
    private void FlipFacedDirection()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //these are triggered by the pickups
    public void ShieldPlayer()
    {
        Debug.Log("Shield");
    }

    public void BoostDamage()
    {
        Debug.Log("Power");
    }

    public void Heal()
    {
        Debug.Log("Heal");
    }
}