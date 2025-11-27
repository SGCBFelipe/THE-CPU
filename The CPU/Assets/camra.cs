using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camra : MonoBehaviour
{
    public Transform cameraTransform;
    float cameraSens = 700f;
    float cameraRotation;
    float maxCameraAngle = 60f;
    float minCameraAngle = -60f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSens * Time.deltaTime;

        cameraRotation -= mouseY;
        cameraRotation = Mathf.Clamp(cameraRotation, minCameraAngle, maxCameraAngle);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
