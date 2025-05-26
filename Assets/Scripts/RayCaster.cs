using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Needed for scene loading
public class RayCaster : MonoBehaviour
{

    public GameObject oldTarget;
    public Item oldItem;
    public InventoryUI inventory;
    public GameObject dialogueTextPrefab; // Prefab for the dialogue text
    public GameObject testScreen;
    public Transform canvas; // Reference to the canvas for UI elements
    private Camera camera;


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



            if (hitObject.CompareTag("Interactable"))
            {
                // Get the Item component and the parent object
                Item itemComponent;
                while ((itemComponent = hitObject.GetComponent<Item>()) == null)
                {
                    // If the object is not an Item, get its parent until we find an Item 
                    hitObject = hitObject.transform.parent?.gameObject;
                    if (hitObject == null)
                    {
                        Debug.LogWarning("No parent with Item component found.");
                        return; // Exit if no parent found
                    }
                }

                if (oldTarget != hitObject)
                {
                    if (oldItem != null) toggleHighlight(oldItem, false);
                    toggleHighlight(itemComponent, true);
                }

                if (Mouse.current.leftButton.wasPressedThisFrame
                    && InteractWithItem(itemComponent))
                {
                    // If interaction requires deleting item, reset oldTarget and oldItem
                    oldTarget = null;
                    oldItem = null;
                }

                oldTarget = hitObject;
                oldItem = itemComponent;
            }
            else if (oldTarget != hitObject && oldItem != null)
            {
                toggleHighlight(oldItem, false);
                oldTarget = null;
                oldItem = null;
            }
        }
        else
        {
            if (oldTarget != null)
            {
                toggleHighlight(oldItem, false);
                oldTarget = null;
                oldItem = null;
            }
        }
    }

    private void toggleHighlight(Item item, bool highlight)
    {
        foreach (Renderer rend in item.highlightRenderers)
        {
            if (rend == null) continue; // Skip if no renderer found
            foreach (Material m in rend.materials)
            {
                if (highlight)
                    m.EnableKeyword("_EMISSION");
                else
                    m.DisableKeyword("_EMISSION");
            }
        }
    }

    private bool InteractWithItem(Item item)
    {
        
        switch (item.itemType)
        {
            case Item.ItemType.pickLock:
            case Item.ItemType.key:
                inventory.AddItem(item);
                Debug.Log($"Added {item.itemType} to inventory.");
                Destroy(item.deletableObject);
                Debug.Log($"Item {item.itemType} destroyed.");
                
                return true; // Interaction successful
                
            case Item.ItemType.cabinet:
                
                if (inventory.selectedItem != null &&
                    (inventory.selectedItem.itemType == Item.ItemType.pickLock ||
                    inventory.selectedItem.itemType == Item.ItemType.key))
                {
                    if (inventory.selectedItem != null &&
                        (inventory.selectedItem.itemType == Item.ItemType.pickLock ||
                        inventory.selectedItem.itemType == Item.ItemType.key))
                    {
                        // Logic to open the cabinet
                        Debug.Log($"Opened cabinet with {inventory.selectedItem.itemType}.");
                        SceneManager.LoadScene("2nd scene");  //Name of the scene as argument
                        return true; // Interaction successful
                    }
                    else
                    {
                        Time.timeScale = 0f; // Pause the game
                        GameObject dialog = Instantiate(dialogueTextPrefab, canvas);
                        dialog.GetComponent<TextWriter>().Init("You need a key or a lock pick to open this cabinet.");
                        Debug.LogWarning("No valid item selected to open the cabinet.");
                        Time.timeScale = 1f; // Resume the game
                    }
                }
                
                return false; // Interaction not handled
            default:
                Debug.LogWarning($"Unknown item type: {item.itemType}");
                return false; // Interaction not handled
        }

    }

}