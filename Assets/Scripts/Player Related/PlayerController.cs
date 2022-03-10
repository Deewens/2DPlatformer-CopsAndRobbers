using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public GameObject Shield;
    public bool firepower;

    private Animator _animator;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;
    
    private bool _isFacingRight = true;  // For determining which way the player is currently facing.

    public PlayerAnimStates playerAnimState;
    private static readonly int State = Animator.StringToHash("State");
    private static readonly int IsArmed = Animator.StringToHash("isArmed");

    public float horizontalsSpeed;
    public float jumpVerticalPushOff;

    private float _horizontalInput;

    public LayerMask groundLayer;

    public TextMeshProUGUI stateDebugText;

    [SerializeField] private int maxHealth;
    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public bool _invincibility;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        _animator.SetBool(IsArmed, true);

        _currentHealth = maxHealth;
    }

    public void SavePlayer()
    {
        //SaveSystem.SaveGameData(this);
    }

    public void LoadPlayer()
    {
        GameData data = SaveSystem.LoadGameData();

        transform.position = new Vector3(data.positionPlayer[0], data.positionPlayer[1], data.positionPlayer[2]);
        _currentHealth = data.health;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (_horizontalInput > 0.001f && !_isFacingRight)
                FlipFacedDirection();
            else if (_horizontalInput < -0.001f && _isFacingRight)
                FlipFacedDirection();
        }


        switch (playerAnimState)
        {
            case PlayerAnimStates.Running:
                if (_horizontalInput == 0) playerAnimState = PlayerAnimStates.Idle;
                    break;
            case PlayerAnimStates.Jumping:
                if (_rb.velocity.y < 0)
                    if (IsGrounded())
                    {
                        if (_horizontalInput > 0.001f || _horizontalInput < -0.001f) playerAnimState = PlayerAnimStates.Running;
                        else playerAnimState = PlayerAnimStates.Idle;
                    }
                break;
        }

        //_rb.AddForce(new Vector2(_horizontalInput * horizontalsSpeed, _rb.velocity.y), ForceMode2D.Impulse);
        _rb.velocity = new Vector2(_horizontalInput * horizontalsSpeed, _rb.velocity.y);
        _animator.SetInteger(State, (int)playerAnimState);
    }

    /*
     * Input events handling
    */
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _horizontalInput = movementVector.x;
        if (IsGrounded()) playerAnimState = PlayerAnimStates.Running;
    }

    public void Moving(Vector2 movement)
    {
        if (Time.timeScale == 1)
        {
            _horizontalInput = movement.x;
        }

        if (IsGrounded()) playerAnimState = PlayerAnimStates.Running;
    }

    public void OnJump()
    {
        if (IsGrounded())
        {
            playerAnimState = PlayerAnimStates.Jumping;
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

    public IEnumerator ShieldPlayer()
    {
        Shield.SetActive(true);
        _invincibility = true;
        yield return new WaitForSeconds(5f);
        Shield.SetActive(false);
        _invincibility = false;
    }

    public IEnumerator BoostDamage()
    {
        firepower = true;
        yield return new WaitForSeconds(5f);
        firepower = false;
    }

    public void Heal()
    {
        _currentHealth += 25;
    }

    public void RemoveHealth(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            GameController.instance.RestartLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (GameController.instance != null)
            {
                GameController.instance.changeLevel();
            }
            else
            {
                Debug.Log("Collided with level exit");
            }
        }
    }


}