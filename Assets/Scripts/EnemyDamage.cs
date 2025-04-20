using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int health=100;
    private int maxhealth;
    public Image bar;
    private Renderer enemyRenderer;
    private Color origiColor;
   
    public void Awake()
    {

        if (enemyRenderer == null)
        {
            enemyRenderer = GetComponent<Renderer>();
        }
        if (enemyRenderer != null)
        {
            origiColor = enemyRenderer.material.color;
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
            
            Destroy(gameObject);
        }
    }

    public IEnumerator damageFlash()
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = Color.white*3f;
            yield return new WaitForSeconds(0.1f);
            enemyRenderer.material.color = origiColor;
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
