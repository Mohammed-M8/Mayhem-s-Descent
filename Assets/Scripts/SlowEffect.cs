using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SlowEffect : MonoBehaviour
{
    public float slowFactor = 0.25f;
    public float slowDuration = 2f;
    public GameObject waterfallVFX;

    private bool slowed = false;
    private NavMeshAgent agent;
    private float originalSpeed;
    private GameObject spawnedVFX;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            originalSpeed = agent.speed;
        }
    }

    public void ApplySlow()
    {
        if (!slowed && agent != null)
        {
            StartCoroutine(Slow());
        }
    }

    private IEnumerator Slow()
    {
        slowed = true;
        agent.speed = originalSpeed * slowFactor;

        
        if (waterfallVFX != null)
        {
            Vector3 vfxPos = transform.position + transform.forward * 0.5f + Vector3.up * 2f;
            spawnedVFX = Instantiate(waterfallVFX, vfxPos, Quaternion.identity, transform);
        }

        yield return new WaitForSeconds(slowDuration);

        agent.speed = originalSpeed;
        slowed = false;

        if (spawnedVFX != null)
        {
            Destroy(spawnedVFX);
        }
    }
}
