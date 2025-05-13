// Assets/Scripts/PointPickup.cs
using UnityEngine;

public class PointPickup : MonoBehaviour
{
    [Tooltip("How many points this pickup gives")]
    public int pointValue = 2;

    [Header("Visuals")]
    [Tooltip("Rotation speed (degrees/sec) around each axis")]
    public Vector3 rotationSpeed = new Vector3(0f, 180f, 0f);

    private bool collected;

    public AudioClip collectible;
    void Update()
    {
        // Spin only on the world-Y axis so it's always visible
        float yRot = rotationSpeed.y * Time.deltaTime;
        transform.Rotate(0f, yRot, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;
        PointsManager.Instance?.AddPoints(pointValue);
        if (SoundManager.Instance != null)
        {
            if (collectible != null)
            {
                SoundManager.Instance.PlaySound(collectible);
            }
            
        }
        Destroy(gameObject);
    }
}