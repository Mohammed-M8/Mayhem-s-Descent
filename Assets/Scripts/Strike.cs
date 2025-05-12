using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Damage;
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
       if (other.CompareTag("Enemy"))
        {

            EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
            enemyDamage.takeDamage(Damage);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Chest"))
        {
            // 2) Call TakeHit on the chest’s DestructibleObject
            var chest = other.GetComponent<DestructibleObject>();
            if (chest != null)
                chest.TakeHit();
        }

        // 3) Destroy the projectile in all cases (or only on chest if you like)
        Destroy(gameObject);
    }
}
