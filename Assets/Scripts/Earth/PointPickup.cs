// PointPickup.cs
using UnityEngine;

public class PointPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: award points here if you add a score manager later
            Destroy(gameObject);
        }
    }
}
