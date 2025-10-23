using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public LayerMask layerMask;

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


        if (!GetComponent<camera>().inventory)
        {
            controller.Move(finalVelocity * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 7f;
        }
        else
        {
            moveSpeed = 5f;
           
        }

        
        
    }
    private void FixedUpdate()
    {
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

        {
            Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
