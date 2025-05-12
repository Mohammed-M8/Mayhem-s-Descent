// Assets/Scripts/EnemyFollow.cs
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damagePerSecond = 50f;  // HP lost per second while touching
    public float followRange = 15f;
    public float attackRange = 2f;

    GameObject player;
    NavMeshAgent agent;
    Renderer playerRenderer;
    Color playerOriginalColor;

    bool isAttacking = false;
    Coroutine attackRoutine;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

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

        // In range → stop and start continuous damage
        if (dist <= attackRange)
        {
            agent.isStopped = true;
            if (!isAttacking)
                attackRoutine = StartCoroutine(AttackLoop());
        }
        else
        {
            // Left contact → stop coroutine & restore color
            if (isAttacking)
            {
                StopCoroutine(attackRoutine);
                isAttacking = false;
                if (playerRenderer != null)
                    playerRenderer.material.color = playerOriginalColor;
            }

            // Otherwise chase or idle
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
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;

        // persistently red
        if (playerRenderer != null)
            playerRenderer.material.color = Color.red;

        while (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if (PauseManager.IsPaused)
            {
                yield return null;
                continue;
            }
            // calculate frame‐scaled damage
            int dmg = Mathf.Max(1, Mathf.RoundToInt(damagePerSecond * Time.deltaTime));
            player.GetComponent<PlayerHealth>().takeDamage(dmg);

            // next frame
            yield return null;
        }

        // restore original color
        if (playerRenderer != null)
            playerRenderer.material.color = playerOriginalColor;

        isAttacking = false;
    }
}
