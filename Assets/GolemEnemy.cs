using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GolemEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    Animator Golem;

    public Transform player;
    private Transform target;

    public LayerMask isGround, isPlayer;

    public float health;//

    private bool isDead = false;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;


    //States
    public float sightRange, attackRange;
    public bool playerIsSightRange, playerIsAttackRange;
    //Health
    public float startHealth = 100;//
    //EXP
    public int expValue = 50;//

    public GameObject deathEffect;//

    public Image healthBar;//



    void Start()
    {
        health = startHealth;
        Golem = GetComponent<Animator>();
    }
    private void Awake()
    {
        player = GameObject.Find("Overworld Protagonist").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //check sight and attack range.
        playerIsSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerIsAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerIsSightRange && !playerIsAttackRange) Patrolling();
        if (playerIsSightRange && !playerIsAttackRange) Chase();
        if (playerIsSightRange && playerIsAttackRange) Attack();

    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

        Golem.SetBool("isMoving", true);
        Golem.SetBool("isAttacking", false);
        Golem.SetBool("isStill", false);
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }
    private void Chase()
    {
        agent.SetDestination(player.position);
        Golem.SetBool("isMoving", true);
        Golem.SetBool("isAttacking", false);
        Golem.SetBool("isStill", false);
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Golem.SetBool("isAttacking", true);
            Golem.SetBool("isMoving", false);
            Golem.SetBool("isStill", false);
            //Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.Euler(0, 90, 0)).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            Destroy(rb.gameObject, 5f);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        { Invoke(nameof(DestroyEnemy), 5f); EnemySpawner.enemyCount -= 1; }
    }

    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);

    }
    private void DestroyEnemy()
    {
        isDead = true;
        /*

        PlayerStats.Exp += killValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        
        //WaveSpawner.EnemyCount--;
        */
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
