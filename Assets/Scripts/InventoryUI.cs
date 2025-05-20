using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI: MonoBehaviour
{
    private List<Item> itemList = new List<Item>();
    private int maxSlots = 5;
    private int currentSlot = 0;

    public void AddItem(Item item)
    {
        if (currentSlot >= maxSlots)
        {
            Debug.Log("Inventory is full!");
            return;
        }

        itemList.Add(item);
        currentSlot++;
        Debug.Log($"Added {item.name} as {item.itemType} to slot {currentSlot}");
    }
}
