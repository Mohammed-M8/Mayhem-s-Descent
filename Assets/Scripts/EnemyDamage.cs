using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health;
    private Renderer enemyRenderer;
    private Color origiColor;
    public void Start()
    {
        if (enemyRenderer == null)
        {
            enemyRenderer = GetComponent<Renderer>();
        }
        if (enemyRenderer != null)
        {
            origiColor = enemyRenderer.material.color;
        }
    }
    public void takeDamage(int Damage)
    {
        health -= Damage;
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
}
