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
    private GameObject dialogueSpoken;

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
    public GameObject pauseMenu;

    MovementType moveType = MovementType.Normal;
    private float nextFireTime;
    public float fireRate = 0.6f;

    //Targeting system variables
    private List<GameObject> shootableTargets = new List<GameObject>();
    private int shootableTargetsCount;
    private GameObject currentTarget;
    private bool targetReset;
    public float lockOnAngle = 30f;
    public GameObject targetingCircleIce;
    public GameObject targetingCircleFire;
    private GameObject targetCircle = null;
    
    //New targeting system attempt
    public static List<TargetObjectScript> nearbyTargets = new List<TargetObjectScript>();
    int lockedTarget = 0;
    bool lockedOn;
    TargetObjectScript target;

    private GameObject currentPlayerBullet;

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
            if (Time.timeScale != 0)
            {
                currentMove = ctx.ReadValue<Vector2>();
                movePressed = currentMove.x != 0 || currentMove.y != 0;
            }
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
        lockedOn = false;
        lockedTarget = 0;
    }

    // Shoot projectile (based on fire rate and if holding down shoot button)
    public void ShootProjectile(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            if (nextFireTime < Time.time && context.performed)
            {

                //Debug.Log(playerBullet.GetComponent<PlayerBullet>().GetTarget());
                currentPlayerBullet = Instantiate(playerBullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
                currentPlayerBullet.GetComponent<PlayerBullet>().SwitchType(selectedType);
                if (target != null)
                {
                    currentPlayerBullet.GetComponent<PlayerBullet>().SetTarget(target.gameObject);
                }
                Debug.Log(currentPlayerBullet.GetComponent<PlayerBullet>().GetTarget());
                currentPlayerBullet.GetComponent<PlayerBullet>().Retarget();
                nextFireTime = Time.time + fireRate;
                AudioSource.PlayClipAtPoint(Fireclip, transform.position, 1.0f);
            }
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

    public void NextDialogueOption(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            if (dialogueSpoken != null && context.performed)
            {
                dialogueSpoken.GetComponent<DialogueTrigger>().NextOption();
            }
        }
    }

    public void RestartLevel(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextTarget(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            Debug.Log(nearbyTargets.Count);
            if (lockedTarget >= nearbyTargets.Count - 1)
            {
                lockedTarget = 0;
                target = nearbyTargets[lockedTarget];
            }
            else
            {
                lockedTarget++;
                target = nearbyTargets[lockedTarget];
            }
        }
    }

    public void PauseMenu(InputAction.CallbackContext context)
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
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
        //Debug.Log(nearbyTargets.Count);
        if (CameraFocus && !lockedOn)
        {
            if (nearbyTargets.Count >= 1)
            {
                lockedOn = true;

                //lockedTarget = 0;
                target = nearbyTargets[lockedTarget];
                if (targetCircle == null)
                {
                    switch (selectedType)
                    {
                        case PlayerBullet.BulletType.Fire:
                            if (target.gameObject.tag == "Enemy")
                            {
                                targetCircle = Instantiate(targetingCircleFire, target.gameObject.GetComponent<EnemyAI>().bulletPoint.transform);
                            }
                            else
                            {
                                targetCircle = Instantiate(targetingCircleFire, target.transform);
                            }
                            break;
                        case PlayerBullet.BulletType.Ice:
                            if (target.gameObject.tag == "Enemy")
                            {
                                targetCircle = Instantiate(targetingCircleIce, target.gameObject.GetComponent<EnemyAI>().bulletPoint.transform);
                            }
                            else
                            {
                                targetCircle = Instantiate(targetingCircleIce, target.transform);
                            }
                            break;
                    }
                }
                else
                {
                    targetCircle.transform.position = target.transform.position;
                }
            }
        }
        
        //When you are not focusing and have locked on or there are no targets
        else if((!CameraFocus && lockedOn) || nearbyTargets.Count == 0)
        {
            lockedOn = false;
            Destroy(targetCircle);
            targetCircle = null;
            lockedTarget = 0;
            target = null;
            //Debug.Log("Not targeting");
        }
        if(lockedOn)
        {
            target = nearbyTargets[lockedTarget];
            if (targetCircle == null)
            {
                switch (selectedType)
                {
                    case PlayerBullet.BulletType.Fire:
                        if (target.gameObject.tag == "Enemy")
                        {
                            targetCircle = Instantiate(targetingCircleFire, target.gameObject.GetComponent<EnemyAI>().bulletPoint.transform);
                        }
                        else
                        {
                            targetCircle = Instantiate(targetingCircleFire, target.transform);
                        }
                        break;
                    case PlayerBullet.BulletType.Ice:
                        if (target.gameObject.tag == "Enemy")
                        {
                            targetCircle = Instantiate(targetingCircleIce, target.gameObject.GetComponent<EnemyAI>().bulletPoint.transform);
                        }
                        else
                        {
                            targetCircle = Instantiate(targetingCircleIce, target.transform);
                        }
                        break;
                }
            }
            else
            {
                targetCircle.transform.position = target.transform.position;
            }

            Debug.Log(lockedTarget);
            //Debug.Log(target.gameObject);
            //Debug.Log(Vector3.Distance(transform.position, target.transform.position));
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

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Lava")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetDialogueSpoken(GameObject dialogue)
    {
        dialogueSpoken = dialogue;
    }
}
