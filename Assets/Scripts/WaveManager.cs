// Assets/Scripts/WaveManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Hookups")]
    [SerializeField] EnemySpawner spawner;  // ← drag your EnemySpawner here

    [Header("Wave Setup")]
    [SerializeField] List<Wave> waves;      // ← size = 3, configure in Inspector
    [SerializeField] float totalTime = 360f;// ← 6 minutes

    void Start()
    {
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        float timer = 0f;
        foreach (var wave in waves)
        {
            // switch to this wave’s settings
            spawner.enemyPrefabs = wave.enemies;
            spawner.spawnInterval = wave.spawnInterval;
            spawner.enabled = true;

            float waveEnd = timer + wave.duration;
            while (timer < waveEnd)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        // all waves done → stop spawning
        spawner.enabled = false;
    }

    void Update()
    {
        // hard stop at 6 minutes
        if (Time.timeSinceLevelLoad >= totalTime && spawner.enabled)
        {
            spawner.enabled = false;
            StopAllCoroutines();
        }
    }
}
