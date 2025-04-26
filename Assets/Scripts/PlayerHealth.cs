using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int maxhealth = 200;
    Animator animator;
    private Renderer[] renderers;
    private Color[] originalColors;

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

    }
    public void takeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(damageFlash());
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void addHealth(int h)
    {
        if (health + h <= maxhealth)
        {
            health += h;
        }
    }
    private IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
    public IEnumerator damageFlash()
    {
        if (renderers != null)
        {
            foreach (Renderer r in renderers)
            {
                r.material.color = Color.white * 5f;
            }

            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = originalColors[i];
            }
        }
    }
}
