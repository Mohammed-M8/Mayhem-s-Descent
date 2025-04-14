using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float lifeTime = 0.5f;
    public float speed = 5f;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy after a short time
    }

    void Update()
    {
        // Move forward slightly from the sword's position
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Example: Apply damage to an enemy if it has a health script
        if (other.CompareTag("Enemy"))
        {
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
            }
        }
    }
}
