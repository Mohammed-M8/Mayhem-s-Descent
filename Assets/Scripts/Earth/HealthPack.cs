using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [Header("Heal Settings")]
    [Tooltip("Amount of health to restore on pickup")] public int healAmount = 50;

    [Header("Animation Settings")]
    [Tooltip("Vertical bob amplitude in world units")] public float bobAmplitude = 0.25f;
    [Tooltip("Vertical bob frequency (cycles per second)")] public float bobFrequency = 1f;
    [Tooltip("Rotation speed in degrees per second around each axis")] public Vector3 rotationSpeed = new Vector3(0f, 45f, 0f);

    private Vector3 startPos;

    void Start()
    {
        // Cache starting position for bobbing
        startPos = transform.position;
    }

    void Update()
    {
        // Bob up and down
        float newY = startPos.y + Mathf.Sin(Time.time * bobFrequency * Mathf.PI * 2f) * bobAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Rotate around local axes
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.addHealth(healAmount);
            }
            Destroy(gameObject);
        }
    }
}
