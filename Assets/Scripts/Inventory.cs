using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }
        instance = this;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 12;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Isso não deveria acontecer slots == elementos");
            return;
        }
        if (items.Contains(item))
        {
            if(item.level < 3)
            {
                item.level++;
            }
            return; 
            
        }
        items.Add(item);

        onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback.Invoke();

    }
}
