using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class PlayerLeveling : MonoBehaviour
{
    public float currentLevel = 1f;
    public float currentKills = 0f;
    public float killsPerLevel = 5f;
    public List<UpgradeChoice> upgradeChoices;
    [SerializeField] public GameObject upgradeChoicePrefab;
    [SerializeField] public GameObject screenTintPanel;
    [SerializeField] public TextMeshProUGUI upgradeText;
    public PlayerStatManager playerStatManager;
    public Weapon weapon;
    public Transform upgradeChoicesPanel;

    void Start()
    {
        upgradeText.gameObject.SetActive(false);
        CreateUpgradeChoices();
    }

    void CreateUpgradeChoices()
    {
        upgradeChoices = new List<UpgradeChoice>();

        UpgradeChoice healthUpgradeChoice = new UpgradeChoice();
        healthUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Health + 20", floatValue = 20f });
        upgradeChoices.Add(healthUpgradeChoice);

        UpgradeChoice damageUpgradeChoice = new UpgradeChoice();
        damageUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Damage + 10%", floatValue = 1f });
        upgradeChoices.Add(damageUpgradeChoice);

        UpgradeChoice movementSpeedUpgradeChoice = new UpgradeChoice();
        movementSpeedUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Extra Movement Speed", floatValue = 2f });
        upgradeChoices.Add(movementSpeedUpgradeChoice);

        UpgradeChoice attackSpeedUpgradeChoice = new UpgradeChoice();
        attackSpeedUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Extra Attack Speed", floatValue = 1f });
        upgradeChoices.Add(attackSpeedUpgradeChoice);

        UpgradeChoice lifeRegenerationAmountUpgradeChoice = new UpgradeChoice();
        lifeRegenerationAmountUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "More Life Regeneration", floatValue = 1f });
        upgradeChoices.Add(lifeRegenerationAmountUpgradeChoice);

        UpgradeChoice lifeRegenerationSpeedUpgradeChoice = new UpgradeChoice();
        lifeRegenerationSpeedUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Faster Life Regeneration", floatValue = 0.1f });
        upgradeChoices.Add(lifeRegenerationSpeedUpgradeChoice);

        UpgradeChoice projectileSpeedUpgradeChoice = new UpgradeChoice();
        projectileSpeedUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Faster Bullets", floatValue = 1f });
        upgradeChoices.Add(projectileSpeedUpgradeChoice);

        UpgradeChoice projectileSizeUpgradeChoice = new UpgradeChoice();
        projectileSizeUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Bigger Bullets", intValue = 1 });
        upgradeChoices.Add(projectileSizeUpgradeChoice);

        UpgradeChoice armourUpgradeChoice = new UpgradeChoice();
        armourUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Damage Reduction", floatValue = 2f });
        upgradeChoices.Add(armourUpgradeChoice);

        UpgradeChoice bleedingDamageUpgradeChoice = new UpgradeChoice();
        bleedingDamageUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Enemies take more Damage over Time", floatValue = 1f });
        upgradeChoices.Add(bleedingDamageUpgradeChoice);

        UpgradeChoice bleedingDurationUpgradeChoice = new UpgradeChoice();
        bleedingDurationUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Damage over Time is Longer", floatValue = 2f });
        upgradeChoices.Add(bleedingDurationUpgradeChoice);

        UpgradeChoice criticalChanceUpgradeChoice = new UpgradeChoice();
        criticalChanceUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Increase Critical Strike Chance", floatValue = 0.05f });
        upgradeChoices.Add(criticalChanceUpgradeChoice);

        UpgradeChoice criticalMultiplierUpgradeChoice = new UpgradeChoice();
        criticalMultiplierUpgradeChoice.upgradeOptions.Add(new UpgradeOption { upgradeName = "Increase Critical Strike Damage", floatValue = 0.20f });
        upgradeChoices.Add(criticalMultiplierUpgradeChoice);
    }

    public void EnemyKilled()
    {
        currentKills++;
        if (currentKills >= killsPerLevel * currentLevel)
        {
            LevelUp();
            currentKills = 0f;
        }
    }

    void LevelUp()
    {
        currentLevel++;

        UpgradeChoice choice = upgradeChoices[Random.Range(0, upgradeChoices.Count)];
        string upgradeName = choice.upgradeOptions[Random.Range(0, choice.upgradeOptions.Count)].upgradeName;

        upgradeText.text = upgradeName + " Upgrade";
        ApplyUpgrade(choice);
        StartCoroutine(ShowUpgradeText());
    }

    IEnumerator ShowUpgradeText()
    {
        upgradeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        upgradeText.gameObject.SetActive(false);
    }
    public void ApplyUpgrade(UpgradeChoice choice)
    {
        Debug.Log("sussy");
        foreach (var option in choice.upgradeOptions)
        {
            switch (option.upgradeName)
            {
                case "Health + 20":
                    playerStatManager.IncreaseHealth(option.floatValue);
                    break;
                case "Damage + 10%":
                    weapon.IncreaseBulletDamageUpgrades(option.floatValue);
                    break;
                case "Extra Movement Speed":
                    playerStatManager.IncreaseMovementSpeed(option.floatValue);
                    break;
                case "Extra Attack Speed":
                    weapon.IncreasePlayerAttackSpeedUpgrades(option.floatValue);
                    break;
                case "More Life Regeneration":
                    playerStatManager.IncreaseLifeRegenerationAmount(option.floatValue);
                    break;
                case "Faster Life Regeneration":
                    playerStatManager.IncreaseLifeRegenerationSpeed(option.floatValue);
                    break;
                case "Faster Bullets":
                    weapon.IncreaseBulletSpeedUpgrades(option.floatValue);
                    break;
                case "Bigger Bullets":
                    weapon.IncreaseBulletSpeedUpgrades(option.intValue);
                    break;
                case "Damage Reduction":
                    playerStatManager.IncreaseArmour(option.floatValue);
                    break;
                case "Enemies take more Damage over Time":
                    weapon.BleedDamageUpgrades(option.floatValue);
                    break;
                case "Damage over Time is Longer":
                    weapon.BleedDurationUpgrades(option.floatValue);
                    break;
                case "Increase Critical Strike Chance":
                    weapon.CriticalStrikeChanceUpgrades(option.floatValue);
                    break;
                case "Increase Critical Strike Damage":
                    weapon.CriticalStrikeMultiUpgrades(option.floatValue);
                    break;
            }
        }
    }
}

[System.Serializable]
public class UpgradeOption
{
    public string upgradeName;
    public float floatValue; 
    public int intValue; 
}

[System.Serializable]
public class UpgradeChoice
{
    public List<UpgradeOption> upgradeOptions = new List<UpgradeOption>();
}
