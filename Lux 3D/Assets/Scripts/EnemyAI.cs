using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int speed;
    public int lineOfSight = 10;
    public int attackRange;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject bulletPoint;
    private Transform player;
    float distanceFromPlayer;
    float defaultAcc;
    NavMeshAgent agent;
    public bool showDistance = false;

    void Start()
    {
        // Make sure that the actual player for the ThirdPersonPlayer has the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        defaultAcc = agent.acceleration;
        
        //agent.isStopped = true;
    }

    void Update()
    {
        agent.destination = player.position;
        distanceFromPlayer = agent.remainingDistance;
        // Keep an eye on this if statement
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            agent.acceleration = speed;
            agent.isStopped = false;
            agent.destination = player.position;
        }
        else if (distanceFromPlayer <= attackRange && nextFireTime < Time.time)
        { 
            agent.acceleration = 8000;
            agent.isStopped = true; 
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity); // Spawn boolet
            nextFireTime = Time.time + fireRate; // Sets the firing delay
        }
        else if(distanceFromPlayer > lineOfSight || distanceFromPlayer < attackRange)
        {
            agent.acceleration = 8000;
            agent.isStopped = true;            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}