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
            Destroy(this.gameObject);
     
        }
    }
}
