using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public float bulletSpeed = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime && (transform.parent != null && transform.parent.Find("Rifle") != null))
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Update next allowed fire time
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bullet.transform.Rotate(-90f, -90f, 0f);

            Vector3 localBulletDirection = new Vector3(0, 1, 0);
            Vector3 bulletDirection = firePoint.TransformDirection(localBulletDirection);

            bulletRigidbody.velocity = bulletDirection * bulletSpeed;
        }
    }
}