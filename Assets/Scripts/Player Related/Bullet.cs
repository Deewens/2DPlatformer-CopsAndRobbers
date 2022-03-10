using UnityEngine;

namespace Player_Related
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20;
        public int damage = 40;
        private Rigidbody2D _rb;
    
        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {

            if (gameObject.CompareTag("Bullet")) // If the bullet come from the Player
            {
                if (col.CompareTag("Player") || col.CompareTag("Pickup")) return;

                if (col.CompareTag("Enemy"))
                {
                    EnemyController enemyController = col.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        enemyController.TakeDamage(damage);
                        Destroy(gameObject);
                    }
                }

                if (col.CompareTag("Police Drone"))
                {
                    PoliceDroneController droneController = col.GetComponent<PoliceDroneController>();
                    if (droneController != null) droneController.RemoveHealth(damage);
                    Destroy(gameObject);
                }
            }

            if (gameObject.CompareTag("Enemy Bullet"))
            {
                if (col.CompareTag("Enemy") || col.CompareTag("Police Drone") || col.CompareTag("Pickup")) return;

                if (col.CompareTag("Player"))
                {
                    PlayerController playerController = col.GetComponent<PlayerController>();
                    if (playerController != null)
                        playerController.RemoveHealth(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
