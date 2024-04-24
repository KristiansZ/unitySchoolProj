using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(bulletDamage);
            }
        }
        else if (!other.CompareTag("Player"))
        {
            // If the bullet hits anything else (e.g., terrain, obstacles), destroy the bullet
            Destroy(gameObject);
        }
    }
}