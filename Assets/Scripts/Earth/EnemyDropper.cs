// EnemyDropper.cs
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public string itemName;     // optional label
    public GameObject prefab;      // what to spawn
    [Range(0f, 100f)]
    public float dropChance;   // % chance to drop
}

public class EnemyDropper : MonoBehaviour
{
    [Header("Drop Table Settings")]
    public List<DropItem> dropTable = new List<DropItem>();

    [Tooltip("Where to spawn the drop; leave empty to use enemy position")]
    public Transform dropSpawnPoint;

    [Tooltip("Vertical offset so the drop floats above ground")]
    public float spawnYOffset = 1f;

    /// <summary>
    /// Call this when the enemy dies.
    /// </summary>
    public void HandleDrop()
    {
        float total = 0f;
        foreach (var e in dropTable)
            total += e.dropChance;

        float roll = Random.Range(0f, total), cum = 0f;
        foreach (var entry in dropTable)
        {
            cum += entry.dropChance;
            if (roll <= cum && entry.prefab != null)
            {
                // spawn it
                Vector3 pos = (dropSpawnPoint != null
                    ? dropSpawnPoint.position
                    : transform.position);
                pos.y += spawnYOffset;
                var drop = Instantiate(entry.prefab, pos, entry.prefab.transform.rotation);

                // ensure it has a PointPickup
                if (drop.GetComponent<PointPickup>() == null)
                    drop.AddComponent<PointPickup>();

                break;
            }
        }
    }
}
