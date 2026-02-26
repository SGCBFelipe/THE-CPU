using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionAsset inputAsset;

    private CharacterController controller;

    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction sprintAction;

    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        actionMap = inputAsset.FindActionMap("Player");
        moveAction = actionMap.FindAction("Move");
        sprintAction = actionMap.FindAction("Sprint");
    }

    private void OnEnable() => actionMap.Enable();
    private void OnDisable() => actionMap.Disable();

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (input.x * cameraRight) + (input.y * cameraForward);

        float currentSpeed =
            sprintAction.IsPressed() ? sprintSpeed : walkSpeed;

        // Gravidade
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
}