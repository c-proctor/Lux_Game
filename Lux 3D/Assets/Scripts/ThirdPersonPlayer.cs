using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ThirdPersonPlayer : MonoBehaviour
{
    //public CharacterController controller;
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Animator animator;
    public GameObject playerBullet;
    public GameObject bulletPoint;
    public CharacterController controller;
    private int playerHealth;
    private PlayerBullet.BulletType selectedType = PlayerBullet.BulletType.Fire;

    // By default, it will be private (because it is a class, we don't have to say private beforehand)  
    Vector2 currentMove;
    bool movePressed;

    PlayerInput input;

    public Transform playerCamera;
    float targetAngle;
    float angle;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVel;
    Vector3 moveVector;

    MovementType moveType = MovementType.Normal;
    private float nextFireTime;
    public float fireRate = 0.6f;

    // In case we want more movement types
    public enum MovementType
    {
        Normal,
        Ice
    }

    private void Awake()
    {
        input = new PlayerInput();

        //Leave this be, if it works, it works. Gets movement based inputs
        input.Player.Move.performed += ctx =>
        {
            currentMove = ctx.ReadValue<Vector2>();
            movePressed = currentMove.x != 0 || currentMove.y != 0;
        };
        /*  re-enable AFTER PGF
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            playerHealth = PlayerPrefs.GetInt("PlayerHealth");
        }
        else
        {
            playerHealth = 100;
        }
        */
        playerHealth = 100;
    }

    // Shoot projectile (based on fire rate and if holding down shoot button)
    public void ShootProjectile(InputAction.CallbackContext context)
    {
        if(nextFireTime < Time.time && context.performed)
        {
            playerBullet.GetComponent<PlayerBullet>().SwitchType(selectedType);
            Instantiate(playerBullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
    // For changing weapon type (can setup as an if check when having actual pickups)
    public void WeaponIce(InputAction.CallbackContext context)
    {
        selectedType = PlayerBullet.BulletType.Ice;
    }
    public void WeaponFire(InputAction.CallbackContext context)
    {
        selectedType = PlayerBullet.BulletType.Fire;
    }

    public void RestartLevel(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleRotation()
    {
        /*
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentMove.x, 0f, currentMove.y);

        Vector3 lookAtPos = currentPosition + newPosition;

        transform.LookAt(lookAtPos);
        */
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void HandleMovement()
    {
        moveVector = Vector3.zero;
        Vector3 direction = new Vector3(currentMove.x, 0f, currentMove.y).normalized;
        //Incorporate with animator from this tutorial (https://www.youtube.com/watch?v=IurqiqduMVQ) at ~ 14.30mins
        if (movePressed)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
        if(!controller.isGrounded)
        {
            //direction += Physics.gravity * 0.1f;
            controller.Move(new Vector3(0f, Physics.gravity.y * Time.deltaTime, 0f));
        }
    }

    private void Update()
    {
        //HandleRotation();
        HandleMovement();
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }
    private void OnDisable()
    {
        input.Player.Disable();
    }

    public void TakeHealth(int lostHealth)
    {
        playerHealth -= lostHealth;
        Debug.Log("Player lost health");
    }

    public void AddHealth(int addedHealth)
    {
        playerHealth += addedHealth;
        Debug.Log("Player health added");
    }

    // Return the player health
    public int GetHealth()
    {
        return playerHealth;
    }
}
