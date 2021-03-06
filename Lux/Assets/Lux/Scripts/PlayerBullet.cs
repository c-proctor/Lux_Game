using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public BulletType type = BulletType.Fire;
    Transform target;
    public int speed;
    Rigidbody bulletRB;

    public enum BulletType
    {
        Fire,
        Ice
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        // Finds the player 
        ///Look at this bit later, seems to work, but might not later on...
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // Looks at the player so that it moves in the correct direction in the update (as long as the projectile is a sphere this is all g)
        transform.LookAt(target);
        // Destroy after 2 seconds
        Destroy(this.gameObject, 2);
    }
    void Update()
    {
        // Moves towards the player's position when it was shot
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
