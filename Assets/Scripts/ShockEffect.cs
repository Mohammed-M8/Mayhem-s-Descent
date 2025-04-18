using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : MonoBehaviour
{
    public float chainArea=10f;
    public int shockDamage = 20;
    public float chainduration = 1f;
    public int maxChains = 5;
    public GameObject chain;

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
            if (currentTarget == null)
            {
                yield break;
            }
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
                    CreateLightningEffect(currentTarget.position, nextTarget.position);
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
    void CreateLightningEffect(Vector3 start, Vector3 end)
    {
        GameObject lightning = Instantiate(chain, start, Quaternion.identity);
        LineRenderer lr = lightning.GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
        Destroy(lightning, 0.2f); // Quickly remove it after showing
    }
}
