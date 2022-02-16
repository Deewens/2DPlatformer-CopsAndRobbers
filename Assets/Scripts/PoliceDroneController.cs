using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PoliceDroneController : MonoBehaviour
{
    public float speed = 2;
    public int health = 100;
    
    public PoliceDroneAnimStates animStates;
    private static readonly int State = Animator.StringToHash("State");

    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector2 _dir;
    private Vector2 _previousDir;
    private Vector2 _savedLocalState;
}
