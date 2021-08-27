using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooting : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public GameObject projectile;

    public GameObject enemyFirePoint;

    public int HealthPoint;
    public int ScoreReward;
    public AudioClip DeathAudioClip;

    private Animator animator;

    public GameObject EnemyBoom;
    public GameObject BoomSpawn;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject EnemyMuzzle;

    void Start()
    {
        animator = GetComponent<Animator>();

        EnemyMuzzle.SetActive(false);
    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attck
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // If walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        animator.SetTrigger("NearPlayerTrigger");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Enemy stops moving when attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("ShootTrigger");
            EnemyMuzzle.SetActive(false);
            Instantiate(projectile, enemyFirePoint.transform.position, enemyFirePoint.transform.rotation);


            alreadyAttacked = true;
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            EnemyMuzzle.SetActive(true);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void OnHit(int damage)
    {
        //Debug.Log("Enemy hit");
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {

            Dead();
        }
    }

    private void Dead()
    {
        GameManager.Instance.UpdateScore(ScoreReward, DeathAudioClip);
        Instantiate(EnemyBoom, BoomSpawn.transform.position, BoomSpawn.transform.rotation);
        Destroy(gameObject, 0.1f);
    }
}
