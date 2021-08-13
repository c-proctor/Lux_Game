using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public Transform Target;

    public Transform camTransform;

    Space offsetPositionSpace = Space.Self;

    public Vector3 offset;

    public float SmoothTime = 0.3f;

    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        offset = camTransform.position - Target.position;
    }

    void LateUpdate()
    {
        if(offsetPositionSpace == Space.Self)
        {
            transform.position = Target.TransformPoint(offset);
        }
        else
        {
            transform.position = Target.position + offset;
        }

        transform.LookAt(Target);
    }
}
