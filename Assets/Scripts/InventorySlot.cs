using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button transferButton;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        
    }

    public void OnTransferButton()
    {
        Inventory.instance.Remove(item);
    }
}
