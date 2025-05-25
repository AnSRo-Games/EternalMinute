using UnityEngine;

// Attach this to any pickupable object and assign its UI icon sprite in the Inspector
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        pickLock,
        key
    }

    public ItemType itemType;
    public Sprite icon;
}
