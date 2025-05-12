using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int health = 100;
    private int maxhealth;
    public Image bar;
    private Renderer[] renderers;
    private Color[] originalColors;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            var single = GetComponent<Renderer>();
            if (single != null)
                renderers = new[] { single };
        }

        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;

        maxhealth = health;
    }

    public void takeDamage(int Damage)
    {
        health -= Damage;
        UpdateHealthBar();

        // flash white on hit
        StartCoroutine(damageFlash());

        if (health <= 0)
        {
            // drop loot
            GetComponent<EnemyDropper>()?.HandleDrop();
            // destroy immediately
            Destroy(gameObject);
        }
    }

    public IEnumerator damageFlash()
    {
        foreach (var r in renderers)
            r.material.color = Color.white * 5f;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }

    void UpdateHealthBar()
    {
        if (bar != null)
            bar.fillAmount = (float)health / maxhealth;
    }
}
