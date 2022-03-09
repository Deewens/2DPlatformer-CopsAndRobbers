using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMissileController : MonoBehaviour
{
    [SerializeField] private float launchSpeed;
    [SerializeField] private int damage;
    
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchMissile(Vector2 direction)
    {
        _rb.AddForce(direction * launchSpeed);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        GameObject player = col.gameObject;
        player.GetComponent<PlayerController>().RemoveHealth(damage);
    }
}
