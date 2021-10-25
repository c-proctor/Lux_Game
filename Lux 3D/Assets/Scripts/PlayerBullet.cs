using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public BulletType type = BulletType.Fire;
    GameObject target = null;
    public int speed;
    Rigidbody bulletRB;
    public Material iceMat, fireMat;
    Vector3 targetVec3;

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
        
        //Debug.Log(target);
        // Looks at the player so that it moves in the correct direction in the update (as long as the projectile is a sphere this is all g)
        //Debug.Log(target);
        //transform.LookAt(target.GetComponent<EnemyAI>().bulletPoint.transform); //Leave commented out for time being...
        // Destroy after 2 seconds
        Destroy(gameObject, 2);
    }

    public void Retarget()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(target.transform.up);
            Debug.Log("TARGETING PLAYER!");
            targetVec3 = target.transform.forward;
        }
        else if (target.GetComponent<EnemyAI>() != null)
        {
            transform.LookAt(target.GetComponent<EnemyAI>().bulletPoint.transform);
        }
        else
        {
            transform.LookAt(target.transform);
        }
        
        //Debug.Log(target);
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
        // transform.position += targetVec3 * speed * Time.deltaTime;
        
        if (target.GetComponent<ThirdPersonPlayer>() == null)
        {
            Debug.Log("This is using not null and is: " + target);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position += targetVec3 * speed * Time.deltaTime;
        }
        
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
        else if(collider.gameObject.GetComponent<ThirdPersonPlayer>() == null)
        {
            Debug.Log(collider.gameObject);
            Destroy(gameObject);
        }
        else if(collider.gameObject.GetComponent<DialogueTrigger>() == null && collider.gameObject.GetComponent<ThirdPersonPlayer>() == null)
        {
            Destroy(gameObject);
        }
        
    }

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget;
        }
    }

    public GameObject GetTarget()
    {
        return (target);
    }
}
