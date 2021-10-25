using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    public int speed;
    Rigidbody bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        // Finds the player 
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().bulletPoint.transform;
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

    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag.Equals("Player") && other.GetType() == typeof(MeshCollider))
        {
            other.gameObject.GetComponent<ThirdPersonPlayer>().TakeHealth(10);
            Destroy(gameObject);
        }
        else if(!other.tag.Equals("Enemy") && !(other.GetType() == typeof(SphereCollider) && other.tag.Equals("Player")))
        {
            if(other.gameObject.GetComponent<ThirdPersonPlayer>() != null)
            {
                other.gameObject.GetComponent<ThirdPersonPlayer>().TakeHealth(10);
            }
            Destroy(gameObject);
        }
    }
}
