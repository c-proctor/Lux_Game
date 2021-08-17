using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class ThirdPersonPlayer : MonoBehaviour
{
    //public CharacterController controller;
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Animator animator;

    // By default, it will be private (because it is a class, we don't have to say private beforehand)  
    Vector2 currentMove;
    bool movePressed;

    PlayerInput input;

    public Transform playerCamera;

    MovementType moveType = MovementType.Normal;

    // In case we want more movement types
    public enum MovementType
    {
        Normal,
        Ice
    }

    private void Awake()
    {
        input = new PlayerInput();

        input.Player.Move.performed += ctx =>
        {
            currentMove = ctx.ReadValue<Vector2>();
            movePressed = currentMove.x != 0 || currentMove.y != 0;
        };

        //controller = this.GetComponent<CharacterController>();
    }

    private void HandleRotation()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentMove.x, 0f, currentMove.y);

        Vector3 lookAtPos = currentPosition + newPosition;

        transform.LookAt(lookAtPos);
    }

    private void HandleMovement()
    {
        //Incorporate with animator from this tutorial (https://www.youtube.com/watch?v=IurqiqduMVQ) at ~ 14.30mins
        if (movePressed)
        {
            transform.position = transform.position + transform.forward * Time.deltaTime;
        }
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    /*
    public void Move(InputValue input)
    {
        Vector3 inputVec = input.Get<Vector3>();
        Vector3 moveVector = new Vector3(inputVec.x, 0f, inputVec.z);
        //controller.Move(moveVector * moveSpeed * Time.deltaTime);
    }
    */
    private void OnEnable()
    {
        input.Player.Enable();
    }
    private void OnDisable()
    {
        input.Player.Disable();
    }
}
