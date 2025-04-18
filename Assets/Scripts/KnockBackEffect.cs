using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour
{
    private Rigidbody rb;
    public float knockForce = 5f;
    private bool knocked = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void applyKnockBack(Vector3 sourcePos)
    {
        if (rb != null&&knocked==false)
        {
            Vector3 direction = (transform.position - sourcePos).normalized;
            StartCoroutine(Knock(direction));
        }
    }

    private IEnumerator Knock(Vector3 dir)
    {
        knocked = true;
        rb.AddForce(dir * knockForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector3.zero;
        knocked = false;
    }

}
