using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;
    public float bleedDamage;
    public float bleedDuration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(bulletDamage);
            }
             if (bleedDamage > 0 && bleedDuration > 0)
            {
                enemyController.StartBleeding(bleedDamage, bleedDuration);
            }
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}