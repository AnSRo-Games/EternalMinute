using UnityEngine;

// Attach this to any pickupable object and assign its UI icon sprite in the Inspector
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        pickLock,
        key,
        cabinet
    }

    public Renderer[] highlightRenderers; // Material to highlight the object

    public GameObject deletableObject; // Object to delete when picked up
    public ItemType itemType;
    public string itemName; // Name of the item
    public Sprite icon;

    void Start()
    {
        // Set the highlight color for the materials, and initialize them to not be highlighted
        foreach (var rend in highlightRenderers)
        {
            foreach (var mat in rend.materials)
            {
                mat.DisableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.darkRed);
            }
        }
    }

    public void ShowHint()
    {
        // Show a hint for the item, e.g., by enabling a UI element or displaying a message
        Debug.Log($"Hint: {itemType} is nearby!");
    }

}
