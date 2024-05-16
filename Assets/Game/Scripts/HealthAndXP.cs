using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndXP : MonoBehaviour
{
    [SerializeField] public Image healthImage;
    [SerializeField] public Image xpImage;
    public PlayerStatManager playerStatManager;
    public PlayerLeveling playerLeveling;

    void Update()
    {
        if (playerStatManager != null && healthImage != null)
        {
            float fillAmount = playerStatManager.currentHealth / playerStatManager.maxHealth;
            healthImage.fillAmount = fillAmount;
        }
        if (playerLeveling != null && xpImage != null)
        {
            float fillAmount = (playerLeveling.currentKills / (playerLeveling.killsPerLevel * playerLeveling.currentLevel));
            xpImage.fillAmount = fillAmount;
        }
    }
}
