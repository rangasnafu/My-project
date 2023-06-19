using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public Transform cameraTransform;
    public float crouchHeight = 0.5f;
    public float normalHeight = 1f;
    public float crouchSmoothTime = 0.2f;

    private float xRotation = 0f;
    private Vector3 cameraOriginalPosition;
    private Vector3 cameraCrouchPosition;
    private bool isCrouching = false;
    private Vector3 cameraVelocity = Vector3.zero;

    public GameObject youWinUI;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (youWinUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        cameraOriginalPosition = cameraTransform.localPosition;
        cameraCrouchPosition = cameraTransform.localPosition - new Vector3(0f, crouchHeight, 0f);

        // Set the initial rotation of the camera to match the player's body rotation
        xRotation = playerBody.localEulerAngles.x;
    }

    private void Update()
    {
        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera horizontally
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Handle crouching
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                StartCoroutine(AdjustCameraPosition(cameraCrouchPosition));
            }
            else
            {
                StartCoroutine(AdjustCameraPosition(cameraOriginalPosition));
            }
        }
    }

    private IEnumerator AdjustCameraPosition(Vector3 targetPosition)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            cameraTransform.localPosition = Vector3.SmoothDamp(cameraTransform.localPosition, targetPosition, ref cameraVelocity, crouchSmoothTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}