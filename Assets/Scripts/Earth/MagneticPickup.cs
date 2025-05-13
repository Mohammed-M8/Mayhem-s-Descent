using UnityEngine;

/// <summary>
/// When the player gets within a certain radius, this pickup will move toward the player.
/// It accelerates the closer it gets, and disappears on contact.
/// </summary>
public class MagneticPickup : MonoBehaviour
{
    [Header("Magnetic Settings")]
    [SerializeField, Tooltip("Distance at which the pickup starts moving toward the player")]
    private float attractRadius = 5f;

    [SerializeField, Tooltip("Base speed when at the edge of attractRadius")]
    private float moveSpeed = 2f;

    private Transform player;

    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Current distance
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attractRadius)
        {
            // Compute a t in [0,1], 0=at edge, 1=at player
            float t = 1f - (dist / attractRadius);

            // Speed scales from moveSpeed up to 2×moveSpeed as it approaches the player
            float currentSpeed = moveSpeed * (1f + t);

            // Move toward player
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                currentSpeed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}