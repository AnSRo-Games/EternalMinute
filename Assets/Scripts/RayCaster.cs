using UnityEngine;
using UnityEngine.InputSystem;
public class RayCaster : MonoBehaviour
{

    public Camera camera;
    public GameObject oldTarget;
    public InventoryUI inventory;

    void Awake()
    {
        // Ensure the camera is assigned
        if (camera == null)
        {
            camera = GetComponent<Camera>();
            if (camera == null)
            {
                Debug.LogError("No camera found. Please assign a camera to the RayCaster script.");
            }
        }
    }

    void Start()
    {
        if (inventory == null)
        {
            inventory = FindFirstObjectByType<InventoryUI>();
            if (inventory == null)
            {
                Debug.LogError("InventoryUI not found in scene. Please assign it in the inspector.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            GameObject hitObject = objectHit.gameObject;
            if (oldTarget != null && oldTarget != hitObject)
            {
                Material mat = oldTarget.GetComponent<Renderer>().material;
                mat.DisableKeyword("_EMISSION");
            }

            if (hitObject.CompareTag("Interactable"))
            {
                // Perform any action with the hit object
                Material mat = hitObject.GetComponent<Renderer>().material;
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.darkRed);

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    // Perform any action with the hit object
                    // Check if it has an Item component
                    if (hitObject.GetComponent<Item>() != null)
                    {
                        Destroy(hitObject);
                        Debug.Log($"Item {hitObject.GetComponent<Item>().itemType} destroyed.");
                        inventory.AddItem(hitObject.GetComponent<Item>());
                        Debug.Log($"Added {hitObject.name} to inventory.");
                    }
                }
            }
            oldTarget = hitObject;

            // Destroy(hitObject);

            // Do something with the object that was hit by the raycast.
        }
        else
        {
            if (oldTarget != null)
            {
                Material mat = oldTarget.GetComponent<Renderer>().material;
                mat.DisableKeyword("_EMISSION");
            }
            oldTarget = null;
        }
    }

}
