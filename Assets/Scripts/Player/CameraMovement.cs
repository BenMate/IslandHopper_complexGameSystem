using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 180.0f; 
    public float smoothSpeed = 0.125f;
    public float clampOffset = 50.0f;

    public Vector3 offset = new Vector3(1.5f, 0, 0.25f);

    public Transform target;
    float xRotation;

    void Start()
    {
        //lock Mouse
        Cursor.lockState = CursorLockMode.Locked;    
    }

    void Update()
    { 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -clampOffset, clampOffset);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        target.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;

        transform.LookAt(target);
    }

}
