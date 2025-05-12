using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    public Image cooldownOverlay;
    public float cooldownDuration = 5f;

    private float cooldownTimer = 0f;
    private bool isOnCooldown = false;

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownOverlay.fillAmount = cooldownTimer / cooldownDuration;

            if (cooldownTimer <= 0f)
            {
                cooldownOverlay.fillAmount = 0f;
                isOnCooldown = false;
            }
        }
    }

    public void TriggerCooldown(float duration)
    {
        cooldownDuration = duration;
        cooldownTimer = duration;
        cooldownOverlay.fillAmount = 1f;
        isOnCooldown = true;
    }
}
