// Assets/Scripts/Wave.cs
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;             // ← human-readable label
    public List<GameObject> enemies;    // ← assign prefabs per wave
    public float duration;              // ← how long this wave lasts
    public float spawnInterval = 3f;    // ← optional override
}
