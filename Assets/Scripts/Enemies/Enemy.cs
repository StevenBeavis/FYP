using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    Animator Grunt;

    public Transform player;
    private Transform target;

    public LayerMask isGround, isPlayer;

    

    public float health;//
    public int EnemyID;

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
    public float timeBetweenBlastAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    [SerializeField] private GameObject aoeEffect;
    public AudioClip Explode;
    
    public AudioClip Axe;
    


    //States
    public float sightRange, attackRange, closeRange;
    public bool playerIsSightRange, playerIsAttackRange, playerIsCloseRange;
    //Health
    public float startHealth = 100;//
    //EXP
    public int expValue = 50;//

    public GameObject deathEffect;//

    public Image healthBar;//



    void Start()
    {
        health = startHealth;
        Grunt = GetComponent<Animator>();
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
        playerIsCloseRange = Physics.CheckSphere(transform.position, closeRange, isPlayer);

        if (!isDead)
        {
            if (!playerIsSightRange && !playerIsAttackRange) Patrolling();
            if (playerIsSightRange && !playerIsAttackRange) Chase();


        

            //Goblin
            if (EnemyID == 1)
            {
                if (playerIsSightRange && playerIsAttackRange) Attack();
            }

            //Golem
            if (EnemyID == 2)
            {
                //if (playerIsSightRange && playerIsAttackRange && !playerIsCloseRange) Attack();
                if (playerIsSightRange && playerIsAttackRange && playerIsCloseRange) Blast();
            
            }

        }
        
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

        Grunt.SetBool("isMoving", true);
        Grunt.SetBool("isAttacking", false);
        Grunt.SetBool("isStill", false);
        if (EnemyID == 2) Grunt.SetBool("isBlast", false);
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
        Grunt.SetBool("isMoving", true);
        Grunt.SetBool("isAttacking", false);
        Grunt.SetBool("isStill", false);
        if (EnemyID == 2) Grunt.SetBool("isBlast", false);

    }
    private void Attack()
    {
        if (EnemyID == 2) Grunt.SetBool("isBlast", false);

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            AudioSource.PlayClipAtPoint(Axe, transform.position);
            Grunt.SetBool("isAttacking", true);
            Grunt.SetBool("isMoving", false);
            Grunt.SetBool("isStill", false);

            //Attack code here
            /*Range attack code
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.Euler(0, 90, 0)).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            Destroy(rb.gameObject, 5f);*/

            //if (rb.gameObject)
            //{
            //    PlayerHealth.takeDamage = true;
            //}
            
            
            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }

    }
    //Grunt Blast (Attack Animation)
    public void MeleeAttack() 
    { 
        PlayerHealth.takeDamage = true;

    }
    public void CheckForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 14f);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<PlayerHealth>()) 
            {
                PlayerHealth.takeDamage = true;
            }
        }
    }
    //Golem Blast (Victory Animation)
    public void BlastAttack()
    {
        //Sphere Overlap
        CheckForPlayer();

        if (EnemyID == 2)
        {
            AudioSource.PlayClipAtPoint(Explode, transform.position);
            aoeEffect.SetActive(true);
            StartCoroutine(AttackEffect());
        }

    }
    
    private IEnumerator AttackEffect()
    {
        yield return new WaitForSeconds(0f);
        aoeEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        aoeEffect.SetActive(false);

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
        { Invoke(nameof(DestroyEnemy), 2f);EnemySpawner.enemyCount -= 1;Grunt.SetBool("isDead", true);isDead = true;PlayerExp.BasicExp = true;  }
    }

    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);

    }

    public void Blast()
    {
        if (!alreadyAttacked)
        {
            Grunt.SetBool("isAttacking", true); Grunt.SetBool("isMoving", false); Grunt.SetBool("isStill", false); Grunt.SetBool("isBlast", true);

            agent.SetDestination(transform.position);

            transform.LookAt(player);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenBlastAttacks);
        }
    }

    
    private void DestroyEnemy()
    {
        
        
        
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
