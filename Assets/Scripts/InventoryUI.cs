using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryUI: MonoBehaviour
{
    private List<Image> slotImages;
    private List<Image> slotBackgrounds;
    private Item[] slotItems;
    public Item SelectedItem { get; private set; }
    private int maxSlots = 5;
    private int selectedSlot = -1;
    private Color normalColor = Color.white;
    private Color selectedColor = new Color(0.7f, 1f, 0.7f, 1f);

    void Awake()
    {
        slotImages = new List<Image>();
        slotBackgrounds = new List<Image>();
        slotItems = new Item[maxSlots];
        for (int i = 1; i <= maxSlots; i++)
        {
            Transform slotTransform = transform.Find($"InventorySlot{i}");
            if (slotTransform != null)
            {
                Image bg = slotTransform.GetComponent<Image>();
                if (bg != null)
                    slotBackgrounds.Add(bg);
                else
                    Debug.LogWarning($"No Image component found on InventorySlot{i}");
                Transform itemTransform = slotTransform.Find("InventoryItem");
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
            else
            {
                Debug.LogWarning($"InventorySlot{i} not found on {name}");
            }
        }
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;
        for (int i = 0; i < slotBackgrounds.Count; i++)
        {
            // Digit1..Digit5 correspond to keys 1-5
            Key key = (Key)((int)Key.Digit1 + i);
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                SelectSlot(i);
            }
        }
    }

    private void SelectSlot(int index)
    {
        // Deselect previous background
        if (selectedSlot >= 0 && selectedSlot < slotBackgrounds.Count)
        {
            slotBackgrounds[selectedSlot].color = normalColor;
        }
        // Select new background
        selectedSlot = index;
        slotBackgrounds[selectedSlot].color = selectedColor;
        // Update selected item reference
        SelectedItem = slotItems[selectedSlot];
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < slotImages.Count; i++)
        {
            if (slotImages[i].sprite == null)
            {
                slotImages[i].sprite = item.icon;
                slotItems[i] = item;
                Debug.Log($"Added {item.itemType} to slot {i + 1}");
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }
}
