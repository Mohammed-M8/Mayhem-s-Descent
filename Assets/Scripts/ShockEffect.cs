using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : MonoBehaviour
{
    public float chainArea=10f;
    public int shockDamage = 20;
    public float chainduration = 1f;
    public int maxChains = 5;

    private List<Transform> enemiesShocked = new List<Transform>();

    public void ApplyShock(GameObject gameObject)
    {
        StartCoroutine(ChainShock(gameObject));
    }

    private IEnumerator ChainShock(GameObject initialEnemy)
    {
        enemiesShocked.Clear();
        enemiesShocked.Add(initialEnemy.transform);
        int remaining = maxChains;
        Transform currentTarget = initialEnemy.transform;
        EnemyDamage en = initialEnemy.GetComponent<EnemyDamage>();
        if (en != null)
        {
            en.takeDamage(shockDamage);
        }
        while (remaining > 0)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(currentTarget.position, chainArea);
            Transform nextTarget = null;
            foreach (var hit in hitEnemies)
            {
                if (currentTarget == null)
                {
                    yield break;
                }
                if (hit.CompareTag("Enemy") && hit.transform!=currentTarget)
                {
                    nextTarget = hit.transform;
                    break;
                }

            }
            if (nextTarget != null)
            {
                EnemyDamage enemyDamage = nextTarget.GetComponent<EnemyDamage>();
                if (enemyDamage != null)
                {
                    enemyDamage.takeDamage(shockDamage);
                    enemiesShocked.Add(nextTarget);
                }
                currentTarget = nextTarget;
            }
            else
            {
                break;
            }

            remaining--;
            yield return new WaitForSeconds(chainduration);
        }
        
    }
}
