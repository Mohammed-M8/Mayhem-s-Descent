using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damagePerSecond = 50f;
    public float followRange = 15f;
    public float attackRange = 2f;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    Renderer playerRenderer;
    Color playerOriginalColor;

    bool isAttacking = false;
    Coroutine attackRoutine;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (player != null)
        {
            playerRenderer = player.GetComponentInChildren<Renderer>();
            if (playerRenderer != null)
                playerOriginalColor = playerRenderer.material.color;
        }
    }

    void Update()
    {
        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= attackRange)
        {
            agent.isStopped = true;
            if (!isAttacking)
                attackRoutine = StartCoroutine(AttackLoop());
        }
        else
        {
            if (isAttacking)
            {
                StopCoroutine(attackRoutine);
                isAttacking = false;
                if (playerRenderer != null)
                    playerRenderer.material.color = playerOriginalColor;
            }

            if (dist <= followRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }

        // 🔄 Set Speed parameter from NavMeshAgent velocity
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;

        if (playerRenderer != null)
            playerRenderer.material.color = Color.red;

        while (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            int dmg = Mathf.Max(1, Mathf.RoundToInt(damagePerSecond * Time.deltaTime));
            player.GetComponent<PlayerHealth>().takeDamage(dmg);

            yield return null;
        }

        if (playerRenderer != null)
            playerRenderer.material.color = playerOriginalColor;

        isAttacking = false;
    }
}
