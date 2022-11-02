using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public InventorySlot[] magicSlots;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryUI;

    private void Start()
    {
        if(inventorySlots.Length < 12)
        {
            inventorySlots = GlobalController.globalInventorySlots;
            GlobalController.globalInventorySlots.CopyTo(inventorySlots, 0); 
        }
    }

    private void Update()
    {
        if(inventorySlots.Length != 0)
        {
            inventorySlots.CopyTo(GlobalController.globalInventorySlots, 0); 
        }

        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

    }


    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.level < 3)
            {
                itemInSlot.level++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

}
