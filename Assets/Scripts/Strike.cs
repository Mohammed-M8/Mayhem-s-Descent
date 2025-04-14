using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Damage;
    private void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        
       if (collision.collider.CompareTag("Enemy"))
        {

            EnemyDamage enemyDamage = collision.collider.GetComponent<EnemyDamage>();
            enemyDamage.takeDamage(Damage);
            Destroy(this.gameObject);
     
        }
    }
}
