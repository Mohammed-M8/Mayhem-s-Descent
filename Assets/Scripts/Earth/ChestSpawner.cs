using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public static ChestSpawner Instance { get; private set; }

    [Header("Spawn Settings")]
    [Tooltip("Chest prefab with DestructibleObject component")]
    public GameObject chestPrefab;
    [Tooltip("Locations where chests will spawn")]
    public List<Transform> spawnPoints = new List<Transform>();
    [Tooltip("Delay in seconds before a destroyed chest respawns")]
    public float respawnDelay = 60f;

    void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        // Initial spawn at all points
        for (int i = 0; i < spawnPoints.Count; i++)
            SpawnChest(i);
    }

    // Spawn a chest at the given spawnPoints index
    void SpawnChest(int index)
    {
        var point = spawnPoints[index];
        GameObject chest = Instantiate(chestPrefab, point.position, point.rotation);

        // Assign the spawnIndex to the DestructibleObject so it knows where to respawn
        var destructible = chest.GetComponent<DestructibleObject>();
        if (destructible != null)
            destructible.spawnIndex = index;
    }

    // Called by DestructibleObject when a chest breaks
    public void NotifyChestDestroyed(int spawnIndex)
    {
        StartCoroutine(RespawnRoutine(spawnIndex));
    }

    IEnumerator RespawnRoutine(int index)
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnChest(index);
    }
}