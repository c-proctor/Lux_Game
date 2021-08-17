using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        // Make sure that the actual player for the ThirdPersonPlayer has the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.position, transform.position);

        // Keep an eye on this if statement
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= attackRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}