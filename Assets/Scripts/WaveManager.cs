// Assets/Scripts/WaveManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Hookups")]
    [SerializeField]
    private EnemySpawner spawner;    // Enemy spawner reference

    [Header("Wave Setup")]
    [SerializeField]
    private List<Wave> waves;        // Configure waves in Inspector

    [Header("Timing")]
    [Tooltip("Auto-calculated total time of all waves (read-only)")]
    [SerializeField]
    private float totalTime;         // Sum of all wave durations

    void OnValidate()
    {
        // Recompute totalTime whenever waves are changed in Inspector
        totalTime = 0f;
        if (waves != null)
        {
            foreach (var wave in waves)
                totalTime += wave.duration;
        }
    }

    void Start()
    {
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        float timer = 0f;
        foreach (var wave in waves)
        {
            // Configure spawner for this wave
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

        // All waves complete
        spawner.enabled = false;
    }

    void Update()
    {
        // Safety stop after totalTime has passed
        if (Time.timeSinceLevelLoad >= totalTime && spawner.enabled)
        {
            spawner.enabled = false;
            StopAllCoroutines();
        }
    }
}