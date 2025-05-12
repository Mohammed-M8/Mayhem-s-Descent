using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Damage;
    public AudioClip hit;

    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
       if (other.CompareTag("Enemy"))
        {

            EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
            enemyDamage.takeDamage(Damage);

            SoundManager.Instance.PlaySound(hit,0.2f);
            this.gameObject.SetActive(false);

            Destroy(this.gameObject);
     
        }


        if (other.CompareTag("Chest"))
        {
            // 2) Call TakeHit on the chest’s DestructibleObject
            var chest = other.GetComponent<DestructibleObject>();
            if (chest != null)
            {
                SoundManager.Instance.PlaySound(hit,0.2f);

                chest.TakeHit();
            }
        }

        // 3) Destroy the projectile in all cases (or only on chest if you like)
        Destroy(gameObject);
    }
}
