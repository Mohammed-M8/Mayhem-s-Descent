using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public float delayBeforeSpawn = 3f;

    private GameObject currentBoss;

    void Start()
    {
        StartCoroutine(DelayedSpawn());
    }

    public void TriggerSpawn()
    {
        StartCoroutine(DelayedSpawn());
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(delayBeforeSpawn);

        if (bossPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("BossSpawner: Missing bossPrefab or spawnPoint!");
            yield break;
        }

        currentBoss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);

        var boss = currentBoss.GetComponent<BossController>();
        if (boss != null)
        {
            boss.BeginCombat(); 
        }
    }
}

