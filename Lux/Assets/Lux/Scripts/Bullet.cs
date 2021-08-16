using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;
    public int speed;
    Rigidbody bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector3(moveDir.x, moveDir.y,moveDir.z);
        Destroy(this.gameObject, 2);
    }

}
