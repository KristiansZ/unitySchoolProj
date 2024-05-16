using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStatManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float armour = 0f;
    public float movementSpeed = 10f;
    public float lifeRegenerationAmount = 1f;
    public float lifeRegenerationInterval = 1f;
    public KinematicCharacterController.Examples.CharacterController characterController;

    private void Awake()
    {
        currentHealth = maxHealth;

        StartCoroutine(LifeRegenerationCoroutine());

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            characterController = playerObject.GetComponent<KinematicCharacterController.Examples.CharacterController>();
            if (characterController != null)
            {
                characterController.MaxStableMoveSpeed = movementSpeed;
            }
        }
    }

    void Update(){
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseMenu");
        }
    }

    IEnumerator LifeRegenerationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(lifeRegenerationInterval);
            RegenerateLife();
        }
    }

    private void RegenerateLife()
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + lifeRegenerationAmount);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount - armour;
    }

    public void IncreaseHealth(float amount)
    {
        maxHealth += amount;
    }
    public void IncreaseArmour(float amount)
    {
        armour += amount;
    }
    public void IncreaseMovementSpeed(float amount)
    {
        movementSpeed += amount;
    }
    public void IncreaseLifeRegenerationAmount(float amount)
    {
        lifeRegenerationAmount += amount;
    }
    public void IncreaseLifeRegenerationSpeed(float amount)
    {
        lifeRegenerationInterval -= amount;
    }
}