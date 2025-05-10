using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1000;
    public int maxhealth = 1000;
    Animator animator;
    private Renderer[] renderers;
    private Color[] originalColors;

    public Image bar;
    public TMP_Text healthText;

    [Header("Screen Flash")]
    [SerializeField] private Image damageOverlay;    // drag your UI Image here
    [SerializeField] private float overlayMaxAlpha = 0.4f;
    [SerializeField] private float fadeSpeed = 1.5f;

    private float overlayAlpha = 0f;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
        {
            Renderer singleRenderer = GetComponent<Renderer>();
            if (singleRenderer != null)
            {
                renderers = new Renderer[] { singleRenderer };
            }
        }

        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }

        if (damageOverlay != null)
        {
            var c = damageOverlay.color;
            c.a = 0f;
            damageOverlay.color = c;
        }

        UpdateHealthBar();

    }

    public void Update()
    {
        if (damageOverlay != null && overlayAlpha > 0f)
        {
            overlayAlpha = Mathf.MoveTowards(overlayAlpha, 0f, fadeSpeed * Time.deltaTime);
            var c = damageOverlay.color;
            c.a = overlayAlpha;
            damageOverlay.color = c;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        UpdateHealthBar();

        if (damageOverlay != null)
        {
            overlayAlpha = overlayMaxAlpha;
            var c = damageOverlay.color;
            c.a = overlayAlpha;
            damageOverlay.color = c;
        }

        StartCoroutine(damageFlash());
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void addHealth(int h)
    {
       
        health = Mathf.Min(health + h, maxhealth);
        UpdateHealthBar();
    }
    private IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
    public IEnumerator damageFlash()
    {
        if (renderers != null)
        {
            foreach (Renderer r in renderers)
            {
                r.material.color = Color.red * 10f;
            }

            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = originalColors[i];
            }
        }
    }

    void UpdateHealthBar()
    {
        if (bar != null)
        {
            bar.fillAmount = (float)health / maxhealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{health} / {maxhealth}";
        }
    }
}
