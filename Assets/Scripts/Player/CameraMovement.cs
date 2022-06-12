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

    public Transform playerBody;
    float xRotation;

    void Start()
    {
        //lock mouse
        Cursor.lockState = CursorLockMode.Locked;
        //start camera in players pos
        transform.position = new Vector3(playerBody.transform.localPosition.x + xOffset, playerBody.transform.localPosition.y + yOffSet, playerBody.transform.localPosition.z + zOffset);

    }

    void Update()
    {
        //be able to update cameras pos
        transform.localPosition = new Vector3(xOffset, yOffSet, zOffset);


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -clampOffset, clampOffset);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
