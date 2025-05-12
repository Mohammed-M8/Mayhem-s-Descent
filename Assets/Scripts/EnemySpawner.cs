// Assets/Scripts/EnemySpawner.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public Camera mainCam;                    // ← drag Main Camera here
    public Terrain terrain;                   // ← drag your Terrain here
    public List<GameObject> enemyPrefabs = new();  // ← drag all your enemy prefabs here

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;  // seconds between spawn attempts
    public float spawnBuffer = 2f;  // how far (world units) outside the view

    float timer;

    void Reset()
    {
        mainCam = Camera.main;
        terrain = Terrain.activeTerrain;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < spawnInterval) return;
        timer = 0f;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0) return;
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // compute viewport‐space margins based on world‐unit buffer
        float camHeight = mainCam.orthographicSize * 2f;
        float camWidth = camHeight * mainCam.aspect;
        float marginY = spawnBuffer / camHeight;
        float marginX = spawnBuffer / camWidth;

        // a horizontal plane at terrain base
        Plane ground = new Plane(Vector3.up,
            new Vector3(0, terrain.transform.position.y, 0));

        for (int i = 0; i < 10; i++)
        {
            float vx = 0f, vy = 0f;
            switch (Random.Range(0, 4))
            {
                case 0: // top
                    vx = Random.Range(0f, 1f);
                    vy = 1f + marginY;
                    break;
                case 1: // bottom
                    vx = Random.Range(0f, 1f);
                    vy = -marginY;
                    break;
                case 2: // left
                    vx = -marginX;
                    vy = Random.Range(0f, 1f);
                    break;
                default: // right
                    vx = 1f + marginX;
                    vy = Random.Range(0f, 1f);
                    break;
            }

            // shoot a ray from that off‐screen viewport point
            Ray ray = mainCam.ViewportPointToRay(new Vector3(vx, vy, 0f));
            if (!ground.Raycast(ray, out float dist))
                continue;

            Vector3 worldPoint = ray.GetPoint(dist);

            // ensure it actually hits within your terrain bounds
            float minX = terrain.transform.position.x;
            float maxX = minX + terrain.terrainData.size.x;
            float minZ = terrain.transform.position.z;
            float maxZ = minZ + terrain.terrainData.size.z;

            if (worldPoint.x < minX || worldPoint.x > maxX ||
                worldPoint.z < minZ || worldPoint.z > maxZ)
                continue;

            // snap to NavMesh (must be walkable)
            if (NavMesh.SamplePosition(worldPoint, out var hit, spawnBuffer, NavMesh.AllAreas))
            {
                Vector3 spawnPos = hit.position;
                spawnPos.y = terrain.SampleHeight(spawnPos);
                Instantiate(prefab, spawnPos, Quaternion.identity);
                return;
            }
        }
        // if all 10 tries fail, just skip this spawn tick
    }
}