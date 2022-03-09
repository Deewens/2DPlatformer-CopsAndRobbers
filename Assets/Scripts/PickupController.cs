using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public enum PickupType
    {
        health,
        damage,
        defense,
        coin
    }
    private PlayerController player;
    public PickupType boost;
    private Renderer rend;

    private void Awake()
    {
        SaveSystem.pickups.Add(this);
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            if (player != null)
            {
                switch (boost)
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
                    case PickupType.coin:
                        if (GameController.instance != null)
                        {
                            GameController.instance.updateScore(100);
                        }
                        break;
                }
            }
            this.gameObject.SetActive(false);
        }
        rend.enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 10);
    }
}
