using UnityEngine;

/// <summary>
/// When the player gets within a certain radius, this pickup will move toward the player.
/// Disappears when it actually contacts the player.
/// </summary>
public class MagneticPickup : MonoBehaviour
{
    [Header("Magnetic Settings")]
    [SerializeField, Tooltip("Distance at which the pickup starts moving toward the player")]
    private float attractRadius = 5f;

    [SerializeField, Tooltip("Speed at which the pickup moves toward the player")]
    private float moveSpeed = 5f;

    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null)
            return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attractRadius)
        {
            // Move toward the player's position
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}