using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health;
 

   public void takeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
