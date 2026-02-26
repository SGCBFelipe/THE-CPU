using UnityEngine;
using UnityEngine.InputSystem;

public class LookCamera : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private Transform _playerBody; // Player (rotação Y)
    [SerializeField] private float _mouseSensitivity = 10f;

    private InputAction _lookAction;
    private InputActionMap _actionMap;

    private float _xRotation = 0f;

    private void Awake()
    {
        _actionMap = _inputAsset.FindActionMap("Player");
        _lookAction = _actionMap.FindAction("Look");
    }

    private void OnEnable()
    {
        _actionMap.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        _actionMap.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Vector2 mouseInput = _lookAction.ReadValue<Vector2>();

        float mouseX = mouseInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * _mouseSensitivity * Time.deltaTime;

        // Rotação vertical (CameraOffset)
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Rotação horizontal (Player)
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}