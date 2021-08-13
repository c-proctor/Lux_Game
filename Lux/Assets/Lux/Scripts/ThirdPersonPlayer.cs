using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Animator animator;

    // By default, it will be private (because it is a class, we don't have to say private beforehand)  
    float moveX;
    float moveZ;

    public Transform playerCamera;

    MovementType moveType = MovementType.Normal;

    // In case we want more movement types
    public enum MovementType
    {
        Normal,
        Ice
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, 0f, moveZ);//.normalized; //Don't need normalized (makes movement slidey). I mean, it may
                                                           // work with the ice mechanic...
        if(moveVector.magnitude >= 0.1f)
        {
            controller.Move(moveVector * moveSpeed * Time.deltaTime);
        }
    }
}
