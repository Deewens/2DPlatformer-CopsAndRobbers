using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMissileController : MonoBehaviour
{
    [SerializeField] private float launchSpeed;
    
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchMissile(Vector2 direction)
    {
        _rb.AddForce(direction * launchSpeed);
    }
}
