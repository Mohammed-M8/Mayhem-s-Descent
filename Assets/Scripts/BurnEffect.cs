using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    public float burnTime = 3f;
    public float burnIntervals = 1f;
    public int burnDamage = 10;
    private bool burning=false;
    private EnemyDamage EnemyDamage;
    private void Awake()
    {
        EnemyDamage = GetComponent<EnemyDamage>();
    }

    public void Burn()
    {
        if (burning == false)
        {
            StartCoroutine(burn());
        }
    }
    private IEnumerator burn()
    {
        burning = true;
            float timePassed = 0f;
            while (timePassed < burnTime)
            {
            if (EnemyDamage != null)
            {
               EnemyDamage.takeDamage(burnDamage);
            }
                yield return new WaitForSeconds(burnIntervals);
                timePassed += burnIntervals;

            }
        burning = false;
        
    }
}
