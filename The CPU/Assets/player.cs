using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour { 

    CharacterController controller;
    public float moveSpeed;
    public Transform head;



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float forwardinput = Input.GetAxisRaw("Horizontal");
        float strafeinput = Input.GetAxisRaw("Vertical");


        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;

        Vector3 finalVelocity = (forwardinput * cameraRight) + (strafeinput * cameraForward);

        controller.Move(finalVelocity * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            moveSpeed = 7f;
        }
        else
        {
            moveSpeed = 5f;
        }


    }
}
