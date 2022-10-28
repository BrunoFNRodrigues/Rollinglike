using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public GameObject inSceneObject;
    // Start is called before the first frame update
    public void PickUp()
    {
        Debug.Log("Picking up "+ item.name);
        //add to inventory
        Inventory.instance.Add(item);
        Destroy(inSceneObject);
    }


}
