using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public int damage = 100;
    GameObject player;
    public float followRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private bool isAttacking = false;
    private Animator animator;

    private void Awake()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Transform play = null;
       
        if (player != null)
        {
            play = player.transform;

        }
        if (play != null)
        {
            float distance = Vector3.Distance(transform.position, play.position);


            if (distance <= attackRange)
            {
                agent.isStopped = true;

                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else if (distance <= followRange)
            {
                agent.isStopped = false;
                agent.SetDestination(play.position);
                float s = agent.velocity.magnitude;
                animator.SetFloat("Speed", s);
            }
            else
            {
                agent.isStopped = true; // Player too far, stop chasing
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerHealth>().takeDamage(damage);

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
}

