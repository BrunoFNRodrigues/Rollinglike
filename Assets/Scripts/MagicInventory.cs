using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicInventory : MonoBehaviour
{
    public static MagicInventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }
        instance = this;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 6;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Inventario cheio");
            return;
        }
        if(item != null)
        {
            items.Add(item);
        }


        onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback.Invoke();

    }
}
