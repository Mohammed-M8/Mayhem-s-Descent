// EnemySpawner.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera mainCam;                     // ← drag your Main Camera here
    [SerializeField] Terrain terrain;                    // ← drag your Terrain here
    [SerializeField] List<GameObject> enemyPrefabs = new();  // ← add all your enemy prefab assets

    [Header("Spawn Settings")]
    [SerializeField] float spawnInterval = 3f;   // seconds between spawn attempts
    [SerializeField] float spawnBuffer = 2f;   // how far outside the view to spawn

    float timer;

    void Reset()
    {
        // auto-assign if you forget
        mainCam = Camera.main;
        terrain = Terrain.activeTerrain;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0) return;

        Bounds camBounds = GetCameraWorldBounds();
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Vector3 spawnPos = Vector3.zero;

        // try up to 10 times to find a valid off-screen point
        for (int i = 0; i < 10; i++)
        {
            Vector3 rawPos = GetPositionOutsideView(camBounds);

            // snap onto NavMesh
            NavMeshHit hit;
            if (!NavMesh.SamplePosition(rawPos, out hit, 2f, NavMesh.AllAreas))
                continue;

            // convert to viewport coords
            Vector3 vp = mainCam.WorldToViewportPoint(hit.position);

            bool inFront = vp.z > 0;
            bool onScreenX = vp.x >= 0 && vp.x <= 1;
            bool onScreenY = vp.y >= 0 && vp.y <= 1;

            // ensure it's off-screen
            if (!(inFront && onScreenX && onScreenY))
            {
                spawnPos = hit.position;
                break;
            }
        }

        if (spawnPos != Vector3.zero)
        {
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        // else: all attempts landed on-screen or off-mesh; skip this tick
    }

    Bounds GetCameraWorldBounds()
    {
        float height = 2f * mainCam.orthographicSize;
        float width = height * mainCam.aspect;
        return new Bounds(mainCam.transform.position, new Vector3(width, 0, height));
    }

    Vector3 GetPositionOutsideView(Bounds b)
    {
        // terrain edges
        float minX = terrain.transform.position.x;
        float maxX = minX + terrain.terrainData.size.x;
        float minZ = terrain.transform.position.z;
        float maxZ = minZ + terrain.terrainData.size.z;

        Vector3 p;
        switch (Random.Range(0, 4))
        {
            case 0: // Top
                p = new Vector3(Random.Range(b.min.x, b.max.x), 0, b.max.z + spawnBuffer);
                break;
            case 1: // Bottom
                p = new Vector3(Random.Range(b.min.x, b.max.x), 0, b.min.z - spawnBuffer);
                break;
            case 2: // Left
                p = new Vector3(b.min.x - spawnBuffer, 0, Random.Range(b.min.z, b.max.z));
                break;
            default: // Right
                p = new Vector3(b.max.x + spawnBuffer, 0, Random.Range(b.min.z, b.max.z));
                break;
        }

        // clamp to terrain bounds
        p.x = Mathf.Clamp(p.x, minX, maxX);
        p.z = Mathf.Clamp(p.z, minZ, maxZ);
        p.y = terrain.SampleHeight(p);

        return p;
    }
}
