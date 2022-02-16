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
    private Renderer rend;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rend = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            switch(boost)
            {
                case PickupType.health:
                    player.Heal();
                    break;
                case PickupType.damage:
                    player.BoostDamage();
                    break;
                case PickupType.defense:
                    StartCoroutine(player.ShieldPlayer());
                    break;
            }
            
        }
        rend.enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 10);
    }
}
