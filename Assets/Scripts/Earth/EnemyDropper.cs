// EnemyDropper.cs
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    [Tooltip("Name for this drop (optional)")]
    public string itemName;

    [Tooltip("Prefab to spawn as a drop (optional)")]
    public GameObject prefab;

    [Tooltip("Chance (0-100) for this item to drop")]
    [Range(0f, 100f)]
    public float dropChance;

    [Tooltip("Point value awarded when this item drops (for future use)")]
    public int pointValue;
}

public class EnemyDropper : MonoBehaviour
{
    [Header("Drop Table Settings")]
    [Tooltip("Configure drop items, chances, and point values in the Inspector.")]
    public List<DropItem> dropTable = new List<DropItem>();

    [Tooltip("Optional: where to spawn the drop prefab")]
    public Transform dropSpawnPoint;

    [Tooltip("Vertical offset (in world units) to raise the spawned drop")]
    public float spawnYOffset = 1f;

    /// <summary>
    /// Call this method when the enemy dies to process drops.
    /// </summary>
    public void HandleDrop()
    {
        // Calculate total combined chance
        float totalChance = 0f;
        foreach (var entry in dropTable)
            totalChance += entry.dropChance;

        // Roll a random value within the total chance
        float roll = Random.Range(0f, totalChance);
        float cumulative = 0f;

        // Find which item corresponds to the rolled value
        foreach (var entry in dropTable)
        {
            cumulative += entry.dropChance;
            if (roll <= cumulative)
            {
                if (entry.prefab != null)
                {
                    // Determine spawn position
                    Vector3 pos = dropSpawnPoint != null
                        ? dropSpawnPoint.position
                        : transform.position;
                    pos.y += spawnYOffset;

                    // Instantiate the drop
                    GameObject drop = Instantiate(
                        entry.prefab,
                        pos,
                        entry.prefab.transform.rotation
                    );

                    // Add the pickup behavior so it vanishes on player contact
                    drop.AddComponent<PointPickup>();
                }

                break;
            }
        }
    }
}
