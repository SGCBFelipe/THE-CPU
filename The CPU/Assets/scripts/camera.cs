using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject canvas;
    float cameraSens = 700f;
    float cameraRotation;
    float maxCameraAngle = 60f;
    float minCameraAngle = -60f;

    public bool inventory = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSens * Time.deltaTime;

        if (!inventory)
        {
            cameraRotation -= mouseY;
            cameraRotation = Mathf.Clamp(cameraRotation, minCameraAngle, maxCameraAngle);
            cameraTransform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            inventory = !inventory;
            canvas.SetActive(inventory);

            if (inventory)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

    }
}
