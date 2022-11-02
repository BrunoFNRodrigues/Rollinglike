using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public static int globalPlayerHealth;
    public static InventorySlot[] globalInventorySlots = new InventorySlot[12];
    public static InventorySlot[] globalMagicSlots = new InventorySlot[6];
}
