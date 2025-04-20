using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour
{
    private Rigidbody rb;
    public float knockForce = 5f;
    private bool knocked = false;
    public GameObject dust;


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
        GameObject dus=null;
        knocked = true;
        if (dust != null)
        {
            dus = Instantiate(dust, transform.position, Quaternion.identity);
        }
        
            rb.isKinematic = false;
        rb.AddForce(dir * knockForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        knocked = false;
        if(dus!=null)
        Destroy(dus);
    }

}
