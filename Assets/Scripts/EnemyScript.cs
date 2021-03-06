using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    [Tooltip("Speed of the enemy")]
    public float MovementSpeed;

    [Tooltip("Damage on player on touch")]
    public int ContactDamage;

    [Tooltip("Health of the enemy")]
    public int HealthPoint;

    [Tooltip("Score reward for destorying enemy")]
    public int ScoreReward;

    [Tooltip("Sound upon death")]
    public AudioClip DeathAudioClip;

    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    public Animator EnemyAnimator;
    public GameObject EnemyBoomPS;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        EnemyAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (playerTransform != null && !GameManager.Instance.isGameWin)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnemyAnimator.SetTrigger("AttackTrigger");
        }
    }
    

    public void OnHit(int damage)
    {
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {          
            Dead();        
        }
    }

    private void Dead() {
        GameManager.Instance.UpdateScore(ScoreReward, DeathAudioClip);
        Instantiate(EnemyBoomPS, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
