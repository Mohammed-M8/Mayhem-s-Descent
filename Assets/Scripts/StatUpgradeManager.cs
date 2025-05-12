using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatUpgradeManager : MonoBehaviour
{
    [Header("Stat References")]
    public PlayerHealth playerHealth;
    public PlayerMove playerMove;
    public PlayerStats playerStats; // Reference to slash prefab

    [Header("UI")]
    public PointsManager pointsManager;
    public TextMeshProUGUI powerValueText;
    public TextMeshProUGUI hpValueText;
    public TextMeshProUGUI speedValueText;

    [Header("Warnings")]
    public UpgradeWarningText powerWarning;
    public UpgradeWarningText hpWarning;
    public UpgradeWarningText speedWarning;

    [Header("Base Values")]
    private float baseDamage;
    private int baseMaxHealth;
    private float baseSpeed;

    [Header("Upgrade Settings")]
    public int powerPercent = 0;
    public int hpPercent = 0;
    public int speedPercent = 0;
    public int costPerUpgrade = 10;

    private void Start()
    {
        // Store base values for scaling
        baseDamage = playerStats.baseDamage;
        baseMaxHealth = playerHealth.maxhealth;
        baseSpeed = playerMove._speed;
    }

    public void UpgradePower()
    {
        if (powerPercent >= 100) return;

        if (pointsManager.SpendPoints(costPerUpgrade))
        {
            powerPercent++;
            float multiplier = 1f + (powerPercent / 100f);
            playerStats.baseDamage = Mathf.RoundToInt(baseDamage * multiplier);
            powerValueText.text = $"{powerPercent}%";
        }

        else
        {
            if (powerWarning != null)
                powerWarning.Show();
        }
    }

    public void UpgradeHP()
    {
        if (hpPercent >= 100) return;

        if (pointsManager.SpendPoints(costPerUpgrade))
        {
            hpPercent++;
            float multiplier = 1f + (hpPercent / 100f);
            playerHealth.maxhealth = Mathf.RoundToInt(baseMaxHealth * multiplier);
            playerHealth.health = Mathf.Min(playerHealth.health, playerHealth.maxhealth);
            hpValueText.text = $"{hpPercent}%";
            playerHealth.UpdateHealthBar();
        }
        else
        {
            if (hpWarning != null)
                hpWarning.Show();
        }
    }

    public void UpgradeSpeed()
    {
        if (speedPercent >= 100) return;

        if (pointsManager.SpendPoints(costPerUpgrade))
        {
            speedPercent++;
            float multiplier = 1f + (speedPercent / 100f);
            playerMove._speed = baseSpeed * multiplier;
            speedValueText.text = $"{speedPercent}%";
        }
        else
        {
            if (speedWarning != null)
                speedWarning.Show();
        }
    }
}
