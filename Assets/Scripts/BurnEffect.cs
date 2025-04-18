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
    public GameObject Fire;
    public GameObject Firevfx;
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
       
            Firevfx = Instantiate(Fire, transform.position, Quaternion.identity);
        
        float timePassed = 0f;
            while (timePassed < burnTime)
            {
            if (EnemyDamage != null)
            {
               EnemyDamage.takeDamage(burnDamage);
            }
            if (Firevfx != null && this != null) // Added safer check
            {
                Firevfx.transform.position = transform.position;
            }
            yield return new WaitForSeconds(burnIntervals);
                timePassed += burnIntervals;

            }
        burning = false;

        Firevfx.GetComponent<ParticleSystem>().Stop();
        
    }
}
