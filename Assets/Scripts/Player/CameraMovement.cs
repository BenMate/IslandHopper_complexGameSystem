using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 180.0f;
    public float yOffSet = 1.5f;
    public float xOffset = 0;
    public float zOffset = 0.25f;

    public float clampOffset = 90.0f;

    public Transform target;
    float xRotation;

    void Start()
    {
        //lock Mouse
        Cursor.lockState = CursorLockMode.Locked;

        transform.localPosition = new Vector3(target.transform.localPosition.x + xOffset, target.transform.localPosition.y + yOffSet, target.transform.localPosition.z + zOffset);

    }

    void Update()
    {
        transform.localPosition = new Vector3(target.transform.localPosition.x + xOffset, target.transform.localPosition.y + yOffSet, target.transform.localPosition.z + zOffset);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -clampOffset, clampOffset);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        target.Rotate(Vector3.up * mouseX);
    }
}
