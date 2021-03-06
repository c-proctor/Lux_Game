using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject target;
    float orig_z;

    // Start is called before the first frame update
    void Start()
    {
        orig_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, orig_z);
        }
    }
}
