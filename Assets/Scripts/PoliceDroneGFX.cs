using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PoliceDroneGFX : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    
    private bool _isFacingRight = true;  // For determining which way the player is currently facing.


    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f && !_isFacingRight)
        {
            FlipFacedDirection();
        } 
        else if (aiPath.desiredVelocity.x <= -0.01f && _isFacingRight)
        {
            FlipFacedDirection();
        }
    }
    
    /// <summary>
    /// Change the faced direction of the player
    /// </summary>
    private void FlipFacedDirection()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
