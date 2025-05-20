using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI: MonoBehaviour
{
    private List<Item> itemList = new List<Item>();
    private List<Image> slotImages;
    private int maxSlots = 5;

    void Awake()
    {
        slotImages = new List<Image>();
        for (int i = 1; i <= maxSlots; i++)
        {
            Transform itemTransform = transform.Find($"InventorySlot{i}/InventoryItem");
            if (itemTransform != null)
            {
                Image img = itemTransform.GetComponent<Image>();
                if (img != null)
                    slotImages.Add(img);
                else
                    Debug.LogWarning($"No Image component found on InventorySlot{i}/InventoryItem");
            }
            else
            {
                Debug.LogWarning($"InventorySlot{i}/InventoryItem not found on {name}");
            }
        }
    }

    public void AddItem(Item item)
    {
        foreach (var img in slotImages)
        {
            if (img.sprite == null)
            {
                img.sprite = item.icon;
                itemList.Add(item);
                int slotIndex = slotImages.IndexOf(img) + 1;
                Debug.Log($"Added {item.itemType} to slot {slotIndex}");
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }
}
