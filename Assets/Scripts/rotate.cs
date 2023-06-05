using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    //public Vector3 rotationSpeed;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Rotate(rotationSpeed * Time.deltaTime);
    //}

    public float rotationSpeed = 60f;
    public float startAngle = 0f;
    public float endAngle = 270f;

    private bool isRotatingForward = true;
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Calculate the target rotation based on the current direction of rotation
        Quaternion targetRotation = Quaternion.Euler(0f, isRotatingForward ? endAngle : startAngle, 0f);

        // Rotate the object towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the object has reached the target rotation
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            // Reverse the direction of rotation
            isRotatingForward = !isRotatingForward;
        }
    }
}
