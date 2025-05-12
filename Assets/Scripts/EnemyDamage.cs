using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int health=100;
    private int maxhealth;
    public Image bar;
    private Renderer[] renderers;
    private Color[] originalColors;
   
    public void Awake()
    {
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

        maxhealth = health;
        
    }

    
    public void takeDamage(int Damage)
    {
        health -= Damage;
        UpdateHealthBar();

        StartCoroutine(damageFlash());
        if (health <= 0)
        {
            if (PointsManager.Instance != null)
            {
                PointsManager.Instance.AddPoints(100); 
            }

            Destroy(gameObject);
            GetComponent<EnemyDropper>().HandleDrop();
        }
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

    void UpdateHealthBar()
    {
        if (bar != null)
        {
            bar.fillAmount = (float)health / maxhealth;
        }
    }
}
