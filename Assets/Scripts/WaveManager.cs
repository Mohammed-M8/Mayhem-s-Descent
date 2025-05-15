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
        // 1) Spawn each wave in turn
        foreach (var wave in waves)
        {
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

        // 2) Stop spawning
        spawner.enabled = false;

        // 3) Wait until all active enemies are gone
        yield return new WaitUntil(() =>
            GameObject.FindGameObjectsWithTag("Enemy").Length == 0
        );

        // 4) Show victory screen
        var pm = FindObjectOfType<PauseManager>();
        if (pm != null)
            pm.ShowVictoryScreen();
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
