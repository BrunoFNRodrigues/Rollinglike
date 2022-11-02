using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public GameObject inSceneObject;
    public InventoryManager inventoryManager;

    public void PickUp()
    {
        Debug.Log("Picking up "+ item.name);
        //add to inventory
        bool added = inventoryManager.AddItem(item);
        if (added)
        {
            Destroy(inSceneObject);
        }
    }


}
