using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public ItemSO itemSO;
    public int quantity;

    public InventoryItemData(ItemSO item, int quantity = 1)
    {
        this.itemSO = item;
        this.quantity = quantity;
    }
}
