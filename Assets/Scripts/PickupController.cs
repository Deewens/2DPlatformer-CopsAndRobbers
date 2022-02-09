using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public enum PickupType
    {
        health,
        damage,
        defense
    }
    private PlayerController player;
    public PickupType boost;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            switch(boost)
            {
                case PickupType.health:
                    break;
                case PickupType.damage:
                    break;
                case PickupType.defense:
                    player.buffDefense();
                    break;
            }
            
        }
        this.gameObject.SetActive(false);
    }
}
