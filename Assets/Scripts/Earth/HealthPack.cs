// Assets/Scripts/HealthPack.cs
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [Header("Heal Settings")]
    [Tooltip("Amount of health to restore on pickup")]
    public int healAmount = 50;

    [Header("Magnetic Settings")]
    [SerializeField, Tooltip("Distance at which the pack starts moving toward the player")]
    private float attractRadius = 5f;

    [SerializeField, Tooltip("Base speed when at the edge of attractRadius")]
    private float moveSpeed = 5f;

    private Transform player;

    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attractRadius)
        {
            // t goes from 0 (at edge) to 1 (on top of player)
            float t = 1f - (dist / attractRadius);
            // speed ramps from moveSpeed up to 2×moveSpeed as it gets closer
            float currentSpeed = moveSpeed * (1f + t);

            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                currentSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.addHealth(healAmount);

        Destroy(gameObject);
    }
}