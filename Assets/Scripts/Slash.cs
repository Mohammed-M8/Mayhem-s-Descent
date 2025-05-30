using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float lifeTime = 0.5f;
    public float speed = 5f;
    public int damage = 10;
    public AudioClip hit;



    private void Start()
    {
        PlayerStats stats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        if (stats != null)
            damage = stats.baseDamage;


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
                SoundManager.Instance.PlaySound(hit,0.2f);
                enemy.takeDamage(damage);
            }
        }
    }
}
