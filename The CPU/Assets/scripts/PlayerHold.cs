using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHold : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputAsset;

    [Header("Pickup Settings")]
    [SerializeField] private float pickupDistance = 3f;
    [SerializeField] private float holdDistance = 2f;
    [SerializeField] private Transform cameraTransform;

    private InputActionMap actionMap;
    private InputAction holdAction;

    public GameObject heldObject;

    private void Awake()
    {
        actionMap = inputAsset.FindActionMap("Player");
        holdAction = actionMap.FindAction("Hold");
    }

    private void OnEnable()
    {
        actionMap.Enable();
    }

    private void OnDisable()
    {
        actionMap.Disable();
    }

    private void Update()
    {
        if (holdAction.WasPressedThisFrame())
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }

        if (heldObject != null)
        {
            HoldObject();
        }
    }

    private void TryPickup()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.collider.CompareTag("canPickUp"))
            {
                heldObject = hit.collider.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                heldObject.GetComponent<BoxCollider>().enabled = false;
                if (rb != null)
                    rb.isKinematic = true;
            }
        }
    }

    private void HoldObject()
    {
        Vector3 targetPosition =
            cameraTransform.position + cameraTransform.forward * holdDistance;

        // Pega todos os Renderers do objeto (caso tenha vários meshes)
        Renderer[] renderers = heldObject.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            // Começa com o primeiro bounds
            Bounds bounds = renderers[0].bounds;

            // Junta todos os bounds dos filhos
            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }

            // Calcula quanto o pivot está deslocado do centro visual
            Vector3 offset = heldObject.transform.position - bounds.center;

            // Compensa o deslocamento
            targetPosition += offset;
        }

        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * 15f);
    }

    private void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        heldObject.GetComponent<BoxCollider>().enabled = true;
        if (rb != null)
            rb.isKinematic = false;

        heldObject = null;
    }
}