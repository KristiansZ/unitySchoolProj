using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] private AudioSource shootingAudioSource;
    public float recoilAmount = 0.2f;
    public float recoilDuration = 0.1f;
    private float nextFireTime = 0f;
    public float purpleShroomCount = 0;
    public float blueShroomCount = 0;
    public float playerAttackSpeedUpgrades = 0;
    public float bulletDamageUpgrades = 0;
    public float bulletScaleUpgrades = 0;
    public float bulletSpeedUpgrades = 0;
    public float criticalStrikeChanceUpgrades = 0;
    public float criticalStrikeMultiUpgrades = 0;
    public float bulletDamage;
    public float fireRate;
    public float bulletSpeed;
    public int bulletScaleMultiplier = 1;
    public float criticalStrikeChance = 0.05f;
    public float criticalStrikeMultiplier = 2f;
    public float bleedDamage = 1f;
    public float bleedDuration = 0f;


    void Update()
    {
        bulletDamage = (float)(10f + (4 * purpleShroomCount)) * (1 + bulletDamageUpgrades / 10);
        fireRate = (float)((1f + (0.2 * blueShroomCount)) * (1 + playerAttackSpeedUpgrades / 10));
        bulletSpeed = (float)(50f * (1 + bulletSpeedUpgrades / 10));

        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime && (transform.parent != null && transform.parent.Find("Rifle") != null))
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.transform.localScale *= bulletScaleMultiplier;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.bulletDamage = bulletDamage;
            bulletScript.bleedDamage = bleedDamage;
            bulletScript.bleedDuration = bleedDuration;

            float randomValue = Random.value;
            if (randomValue < criticalStrikeChance)
            {
                bulletScript.bulletDamage *= criticalStrikeMultiplier;
            }
        }
        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bullet.transform.Rotate(-90f, -90f, 0f);

            Vector3 localBulletDirection = new Vector3(0, 1, 0);
            Vector3 bulletDirection = firePoint.TransformDirection(localBulletDirection);

            bulletRigidbody.velocity = bulletDirection * bulletSpeed;
        }
     StartCoroutine(HandleRecoil());
    }

    IEnumerator HandleRecoil()
    {
        Vector3 originalLocalPosition = transform.localPosition;
        Vector3 recoilLocalPosition = originalLocalPosition - Vector3.forward * recoilAmount;

        float elapsedTime = 0f;

        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(originalLocalPosition, recoilLocalPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = recoilLocalPosition;

        elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilLocalPosition, originalLocalPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalLocalPosition;
    }
    public void IncreaseAttackSpeedShrooms(float amount)
    {
        blueShroomCount += amount;
    }
    public void IncreaseDamageShrooms(float amount)
    {
        purpleShroomCount += amount;
    }
    public void IncreaseBulletDamageUpgrades(float amount)
    {
        bulletDamageUpgrades += amount;
    }
    public void IncreasePlayerAttackSpeedUpgrades(float amount)
    {
        playerAttackSpeedUpgrades += amount;
    }
    public void IncreaseBulletSpeedUpgrades(float amount)
    {
        bulletSpeedUpgrades += amount;
    }
    public void IncreaseBulletScale(int amount)
    {
        bulletScaleMultiplier += amount;
    }
    public void CriticalStrikeChanceUpgrades(float amount)
    {
        criticalStrikeChanceUpgrades += amount;
    }
    public void CriticalStrikeMultiUpgrades(float amount)
    {
        criticalStrikeMultiplier += amount;
    }
    public void BleedDamageUpgrades(float amount)
    {
        bleedDamage += amount;
    }
    public void BleedDurationUpgrades(float amount)
    {
        bleedDuration += amount;
    }
}