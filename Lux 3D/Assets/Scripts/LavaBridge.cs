using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBridge : MonoBehaviour
{
    private RaycastHit hit;
    public GameObject Bridge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, -Vector3.down, out hit, 5f))
        {
            if(hit.collider.gameObject.tag == "Lava")
            {
                if (Bridge != null)
                {
                    Instantiate(Bridge, hit.point, Quaternion.identity);
                }
            }
        }
    }
}
