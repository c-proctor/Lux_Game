using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private AudioSource Audio;
    public AudioClip Fireclip;
    public AudioClip BackgroundMusic;
    private bool CameraFocus = false;

    // By default, it will be private (because it is a class, we don't have to say private beforehand)  
    Vector2 currentMove;
    bool movePressed;

    PlayerInput input;

    public Transform playerCamera;
    public Camera viewCamera;
    float targetAngle;
    float angle;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3.0f;
    private Vector3 jump;
    private bool jumpPressed = false;
    //private float verticleVelocity = 0f;
    Rigidbody playerRB;
    float turnSmoothVel;
    Vector3 moveVector;

    MovementType moveType = MovementType.Normal;
    private float nextFireTime;
    public float fireRate = 0.6f;

    //Targeting system variables
    private List<GameObject> shootableTargets = new List<GameObject>();
    private int shootableTargetsCount;
    private GameObject currentTarget;
    private bool targetReset;
    public float lockOnAngle = 30f;

    public GameObject currentPlayerBullet;

    // In case we want more movement types
    public enum MovementType
    {
        Normal,
        Ice
    }

    private void Awake()
    {
        input = new PlayerInput();
        playerRB = GetComponent<Rigidbody>();

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
        Audio = GetComponent<AudioSource>();
        Audio.clip = BackgroundMusic;
        Audio.loop = true;
        Audio.Play();
    }

    public void Start()
    {
        jump = new Vector3(0f, jumpHeight, 0f);
    }

    // Shoot projectile (based on fire rate and if holding down shoot button)
    public void ShootProjectile(InputAction.CallbackContext context)
    {
        if(nextFireTime < Time.time && context.performed)
        {
            
            //Debug.Log(playerBullet.GetComponent<PlayerBullet>().GetTarget());
            currentPlayerBullet = Instantiate(playerBullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
            currentPlayerBullet.GetComponent<PlayerBullet>().SwitchType(selectedType);
            currentPlayerBullet.GetComponent<PlayerBullet>().SetTarget(currentTarget);
            currentPlayerBullet.GetComponent<PlayerBullet>().Retarget();
            nextFireTime = Time.time + fireRate;
            AudioSource.PlayClipAtPoint(Fireclip, transform.position, 1.0f);
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

    public void JumpPlayer(InputAction.CallbackContext context)
    {
        //playerRB.AddForce(new Vector3(0f, jumpHeight ,0f),ForceMode.Impulse);
        //jumpPressed = context.performed;
    }

    public void LockCamera(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            CameraFocus = true;
        }
        else if(context.canceled)
        {
            CameraFocus = false;
        }
    }

    private void HandleRotation()
    {
        if (CameraFocus)
        {
            Vector3 direction = new Vector3(currentMove.x, 0f, currentMove.y).normalized;
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
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
            if (animator != null)
            {
                animator.SetFloat("moveSpeed", moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetFloat("moveSpeed", 0.0f);
            }
        }
        if(!controller.isGrounded)
        {
            //direction += Physics.gravity * 0.1f;
            controller.Move(Physics.gravity * Time.deltaTime);
        }
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //using https://amirazmi.net/targeting-system/ tutorial
        if (currentTarget == null)
        {
            targetReset = false;
        }
        if (shootableTargets != null)
        {
            for(int ii = shootableTargets.Count - 1; ii>= 0; --ii)
            {
                if(shootableTargets[ii] == null || !shootableTargets[ii].activeInHierarchy)
                {
                    shootableTargets.RemoveAt(ii);
                    shootableTargetsCount--;
                }
            }
        }
        if(CameraFocus && shootableTargetsCount > 0)
        {
            List<GameObject> SortedShootableTargets = shootableTargets.OrderBy(gameObjects =>
            {
                Vector3 target_direction = gameObjects.transform.position - playerCamera.position;

                var cameraForward = new Vector2(playerCamera.forward.x, playerCamera.forward.z);

                var targetDir = new Vector2(target_direction.x, target_direction.z);

                float angle = Vector2.Angle(cameraForward, targetDir);

                return angle;
            }).ToList();
            for (var ii = 0; ii < shootableTargets.Count(); ii++)
            {
                shootableTargets[ii] = SortedShootableTargets[ii];
                if(!shootableTargets[ii].activeInHierarchy)
                {
                    shootableTargets.RemoveAt(ii);
                    shootableTargetsCount--;
                }
            }
            currentTarget = shootableTargets.First();

            targetReset = !targetReset;
        }
        if(targetReset && shootableTargetsCount > 0 && currentTarget != null && currentTarget.activeInHierarchy)
        {
            Vector3 targetDirection = currentTarget.transform.position - playerCamera.position;

            var cameraForward = new Vector2(playerCamera.forward.x, playerCamera.forward.z);

            var targetDir = new Vector2(targetDirection.x, targetDirection.z);

            float angle = Vector2.Angle(cameraForward, targetDir);

            if (angle < Mathf.Abs(lockOnAngle))
            {
                List<GameObject> SortedShootableTargets = shootableTargets.OrderBy(gameObjects =>
                {
                    Vector3 target_direction3 = gameObjects.transform.position - playerCamera.position;

                    var cameraForward = new Vector2(playerCamera.forward.x, playerCamera.forward.z);

                    var targetDir = new Vector2(target_direction3.x, target_direction3.z);

                    float angle = Vector2.SignedAngle(cameraForward, targetDir);

                    return angle;
                }).ToList();
                for (var ii = 0; ii < shootableTargets.Count(); ++ii)
                {
                    shootableTargets[ii] = SortedShootableTargets[ii];
                }
                if (shootableTargets.IndexOf(currentTarget) >= 0)
                {
                    GameObject nextTarget = shootableTargets[shootableTargets.IndexOf(currentTarget)];

                    Vector3 target_direction3 = nextTarget.transform.position - playerCamera.position;

                    var cameraForward_angled = new Vector2(playerCamera.forward.x, playerCamera.forward.z);

                    var targetDir_angled = new Vector2(target_direction3.x, target_direction3.z);

                    float angled_shot = Vector2.Angle(cameraForward_angled, targetDir_angled);

                    if (angled_shot < Mathf.Abs(lockOnAngle))
                    {
                        currentTarget = shootableTargets[shootableTargets.IndexOf(currentTarget)];
                    }
                }
            }
            else
            {

            }
        }
        else
        {
            if(currentTarget != null)
            {
                currentTarget = null;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        { 
            shootableTargetsCount++;
            shootableTargets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            shootableTargetsCount--;
            shootableTargets.Remove(other.gameObject);
        }
    }
}
