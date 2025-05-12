using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    public float slowAmount = 0.2f;
    public float slowduration = 2f;
    
    private bool slowed = false;


    private void Awake()
    {
        
    }
    public void ApplySlow()
    {
        if (!slowed)
        {
            StartCoroutine(Slow());
        }
    }

    private IEnumerator Slow()
    {
        slowed = true;
        yield return new WaitForSeconds(slowduration);
        slowed = false;
    }
}
