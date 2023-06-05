using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstPersonController : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float crouchSpeed = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float slopeForce = 5f;
    public float slopeRayLength = 1f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;
    private bool isCrouching;

    private bool canMove = true; // Flag to track if the player is allowed to move
    //private bool isCaught = false; // Flag to track if the player has been caught 

    public PlayerSoundManager soundManager;

    public float deathDelay = 2f;

    public GameObject gameOverUI;

    public AudioClip playerCaughtSound;
    private AudioSource audioSource;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        soundManager = GetComponent<PlayerSoundManager>();

        audioSource = GetComponent<AudioSource>();

        if (soundManager == null)
        {
            Debug.LogWarning("PlayerSoundManager component not found on player object");
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float speed = walkSpeed;

        if (isCrouching)
        {
            speed = crouchSpeed;
        }
        else if (isSprinting)
        {
            speed = sprintSpeed;
        }

        float x = 0f; //Input.GetAxis("Horizontal");
        float z = 0f; //Input.GetAxis("Vertical");

        if (canMove) // Only read input and move if the player is allowed to move
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && (x != 0f || z != 0f))
        {
            soundManager.PlayerWalkSound();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            soundManager.PlayerWalkSound();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            soundManager.audioSource.Stop();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        // Check for slope angle
        if ((controller.collisionFlags & CollisionFlags.Below) == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeRayLength))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                if (slopeAngle > controller.slopeLimit)
                {
                    velocity += Physics.gravity * slopeForce * Time.deltaTime;
                }
            }
        }

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            soundManager.PlayerRunSound();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            soundManager.audioSource.Stop();

            //soundManager.PlayerWalkSound();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
        }

        //if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        //
        //    SceneManager.LoadScene("GameOverMenu");
        //}

        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Level1");
        }

        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Menu");
            Cursor.lockState = CursorLockMode.None;
        }

        if (SceneManager.GetActiveScene().name == "Win")
        {
            // Set Cursor.lockState to CursorLockMode.None
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Flashlight"))
        {
            //isCaught = true; // Set the flag to true to indicate that the player has been caught
            canMove = false; // Disable player movement

            //if (isCaught)
            //{
            //    soundManager.PlayerCaughtSound();
            //}


            //soundManager.PlayerCaughtSound();
            //canMove = false; // Set the flag to false to disable player movement

            Time.timeScale = 0f;
            gameOverUI.SetActive(true);

            //SceneManager.LoadScene("GameOverMenu");

            //if (gameOverUI.activeInHierarchy)
            //{
            //    soundManager.PlayerCaughtSound();
            //}

            //soundManager.PlayerCaughtSound();
        }
    }

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.CompareTag("Flashlight"))
     //   {
    //        soundManager.PlayerCaughtSound();
    //    }
    //}

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}