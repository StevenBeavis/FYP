using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class OverworldEnemy : MonoBehaviour 
{
    public NavMeshAgent agent;

    Animator Grunt;

    public Transform player;
    private Transform target;

    public LayerMask isGround, isPlayer;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange=20;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    
    bool isDead = false;

    public float dis;
    bool at = true;

    //States
    public float sightRange, attackRange;
    public bool playerIsSightRange, playerIsAttackRange;

    public string levelToLoad;

    void Start()
    {
        Grunt = GetComponent<Animator>();
    }
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Overworld Protagonist").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //if (player = null) return;
        //check sight and attack range.
        playerIsSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerIsAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerIsSightRange && !playerIsAttackRange) Patrolling();
        if (playerIsSightRange && !playerIsAttackRange) Chase();
        if (playerIsSightRange && playerIsAttackRange) StartCoroutine(Attack());
        

        if (isDead == true) DestroyEnemy();
        dis+=.001f;
    }

    private void Patrolling()
    {
        if (!walkPointSet){ SearchWalkPoint();dis = 0; }

        if (walkPointSet)
         agent.SetDestination(walkPoint); 

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < dis) walkPointSet = false;

        Grunt.SetBool("isMoving", true);
        Grunt.SetBool("isAttacking", false);
        Grunt.SetBool("isStill", false);
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;
    }
    private void Chase()
    {
        agent.SetDestination(player.position);
        Grunt.SetBool("isMoving", true);
        Grunt.SetBool("isAttacking", false);
        Grunt.SetBool("isStill", false);
    }
    private IEnumerator Attack()
    {
        if (at == true)
        {
            
            at = false;
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            Grunt.SetBool("isAttacking", true);
            Grunt.SetBool("isMoving", false);
            Grunt.SetBool("isStill", false);
            
            SaveController.OnEnterBattle();StartCoroutine(DestroyEnemy());BattleTrans.battle = true; 
        

            yield return new WaitForSeconds(1);


        }
        
    }
    
    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
        EnemyOverworldSpawner.enemyCount--;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
