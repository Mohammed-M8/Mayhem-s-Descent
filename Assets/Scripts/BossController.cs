using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public Transform player;

    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    public void ShootProjectile()
    {
        Debug.Log("ShootProjectile called!");

        if (projectilePrefab == null || firePoint == null || player == null) return;
        Vector3 spawnPos = firePoint.position;
        spawnPos.y = 1f;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Vector3 dir = player.position - firePoint.position;
        dir.y = 0f;
        dir.Normalize(); projectile.GetComponent<ProjectileDamage>()?.SetDirection(dir);

    }


    private void Update()
    {
        RotateTowardsPlayer();
    }

    void RotateTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f; // keep the boss upright

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }

    public void BeginCombat()
    {
        StartCoroutine(CastingLoop());
    }

    IEnumerator CastingLoop()
    {
        while (true)
        {
            animator.SetTrigger("Cast");
            yield return new WaitForSeconds(0.2f); // Adjust for attack frequency
        }
    }
}
