using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public BulletType type = BulletType.Fire;
    Transform target;
    public int speed;
    Rigidbody bulletRB;
    public Material iceMat, fireMat;

    public enum BulletType
    {
        None,
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
        Destroy(gameObject, 2);
    }
    public void SwitchType(BulletType bulletType)
    {
        type = bulletType;
        switch (type)
        {
            case BulletType.Fire:
                GetComponent<Renderer>().material = fireMat;
                break;
            case BulletType.Ice:
                GetComponent<Renderer>().material = iceMat;
                break;
        }
    }
    void Update()
    {
        // Moves towards the player's position when it was shot
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collider)
    {
        // If the other object has an ice component while the bullet is Fire type
        if (collider.gameObject.GetComponent<Ice>() != null && type == BulletType.Fire)
        {
            collider.gameObject.GetComponent<Ice>().DestroySelf();
            Destroy(gameObject);
        }
        else if (collider.gameObject.GetComponent<Fire>() != null && type == BulletType.Ice)
        {
            collider.gameObject.GetComponent<Fire>().DestroySelf();
            Destroy(gameObject);
        }
    }
}
