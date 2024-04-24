using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Camera mainCamera;

    public void UpdateHealthBar(float currHealth, float maxHealth)
    {
        slider.value = currHealth / maxHealth;
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (mainCamera != null)
        {
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}