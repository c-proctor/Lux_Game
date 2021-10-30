using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBridge : MonoBehaviour
{
    private RaycastHit hit;
    private RaycastHit bridgeCheckHit;
    public GameObject Bridge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.green);
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.gameObject.tag == "Lava")
            {
                if (Bridge != null || this.gameObject.GetComponent<PlayerBullet>().type == PlayerBullet.BulletType.Ice)
                {
                    Instantiate(Bridge, hit.point - new Vector3(0f, 2.2f, 0f), Quaternion.identity);
                }
            }
        }
    }
}
