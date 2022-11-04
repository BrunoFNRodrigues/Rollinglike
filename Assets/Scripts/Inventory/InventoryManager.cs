using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public InventorySlot[] magicSlots;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryUI;
    public Item[] elements;

    private void Start()
    {
        //Inicializa o espacos de magia
        if (GlobalMagicSlots.fireLevel > 0)
        {
            for (int i = 0; i < GlobalMagicSlots.fireLevel; i++)
            {
                AddMagic(elements[0]);
            }
            GlobalInventory.fireLevel = 0;
        }
        
        if (GlobalMagicSlots.waterLevel > 0)
        {
            for(int i = 0; i < GlobalMagicSlots.waterLevel; i++)
            {
                AddMagic(elements[1]);
            }
            GlobalInventory.waterLevel = 0;
        }

        if (GlobalMagicSlots.earthLevel > 0)
        {
            for (int i = 0; i < GlobalMagicSlots.earthLevel; i++)
            {
                AddMagic(elements[2]);
            }
            GlobalInventory.earthLevel = 0;
        }

        if (GlobalMagicSlots.eletricLevel > 0)
        {
            for (int i = 0; i < GlobalMagicSlots.eletricLevel; i++)
            {
                AddMagic(elements[3]);
            }
            GlobalInventory.eletricLevel = 0;
        }

        if (GlobalMagicSlots.lifeLevel > 0)
        {
            for (int i = 0; i < GlobalMagicSlots.lifeLevel; i++)
            {
                AddMagic(elements[4]);
            }
            GlobalInventory.lifeLevel = 0;
        }

        if (GlobalMagicSlots.deathLevel > 0)
        {
            for (int i = 0; i < GlobalMagicSlots.deathLevel; i++)
            {
                AddMagic(elements[5]);
            }
            GlobalInventory.deathLevel = 0;
        }

        //Inicializa o inventario
        if (GlobalInventory.fireLevel > 0)
        {
            for (int i = 0; i < GlobalInventory.fireLevel; i++)
            {
                AddItem(elements[0]);
            }
        }
        
        if (GlobalInventory.waterLevel > 0)
        {
            for(int i = 0; i < GlobalInventory.waterLevel; i++)
            {
                AddItem(elements[1]);
            }
        }

        if (GlobalInventory.earthLevel > 0)
        {
            for (int i = 0; i < GlobalInventory.earthLevel; i++)
            {
                AddItem(elements[2]);
            }
        }

        if (GlobalInventory.eletricLevel > 0)
        {
            for (int i = 0; i < GlobalInventory.eletricLevel; i++)
            {
                AddItem(elements[3]);
            }
        }

        if (GlobalInventory.lifeLevel > 0)
        {
            for (int i = 0; i < GlobalInventory.lifeLevel; i++)
            {
                AddItem(elements[4]);
            }
        }

        if (GlobalInventory.deathLevel > 0)
        {
            for (int i = 0; i < GlobalInventory.deathLevel; i++)
            {
                AddItem(elements[5]);
            }
        }


    }

    private void Update()
    {
        // Verifica itens no invetario
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if(itemInSlot.item.name == "Fire")
                {
                    GlobalInventory.fireLevel = itemInSlot.level;
                    GlobalMagicSlots.fireLevel = 0;

                }

                if (itemInSlot.item.name == "Water")
                {
                    GlobalInventory.waterLevel = itemInSlot.level;
                    GlobalMagicSlots.waterLevel = 0;

                }

                if (itemInSlot.item.name == "Earth")
                {
                    GlobalInventory.earthLevel = itemInSlot.level;
                    GlobalMagicSlots.earthLevel = 0;

                }

                if (itemInSlot.item.name == "Eletric")
                {
                    GlobalInventory.eletricLevel = itemInSlot.level;
                    GlobalMagicSlots.eletricLevel = 0;

                }

                if (itemInSlot.item.name == "Life")
                {
                    GlobalInventory.lifeLevel = itemInSlot.level;
                    GlobalMagicSlots.lifeLevel = 0;

                }

                if (itemInSlot.item.name == "Death")
                {
                    GlobalInventory.deathLevel = itemInSlot.level;
                    GlobalMagicSlots.deathLevel = 0;

                }
            }
        }

        //Verifica os espacos de magia
        for (int i = 0; i < magicSlots.Length; i++)
        {
            InventorySlot slot = magicSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.item.name == "Fire")
                {
                    GlobalMagicSlots.fireLevel = itemInSlot.level;

                }

                if (itemInSlot.item.name == "Water")
                {
                    GlobalMagicSlots.waterLevel = itemInSlot.level;

                }

                if (itemInSlot.item.name == "Earth")
                {
                    GlobalMagicSlots.earthLevel = itemInSlot.level;

                }

                if (itemInSlot.item.name == "Eletric")
                {
                    GlobalMagicSlots.eletricLevel = itemInSlot.level;

                }

                if (itemInSlot.item.name == "Life")
                {
                    GlobalMagicSlots.lifeLevel = itemInSlot.level;

                }

                if (itemInSlot.item.name == "Death")
                {
                    GlobalMagicSlots.deathLevel = itemInSlot.level;

                }
            }
        }

        //Abre\Fecha o inventario
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

    }


    public bool AddItem(Item item)
    {
        for (int i = 0; i < magicSlots.Length; i++)
        {
            InventorySlot slot = magicSlots[i];
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

    public bool AddMagic(Item item)
    {
        for (int i = 0; i < magicSlots.Length; i++)
        {
            InventorySlot slot = magicSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.level < 3)
            {
                itemInSlot.level++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < magicSlots.Length; i++)
        {
            InventorySlot slot = magicSlots[i];
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
