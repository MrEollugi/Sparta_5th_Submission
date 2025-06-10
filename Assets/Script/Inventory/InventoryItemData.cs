using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public ItemSO itemSO;
    public int quantity;
    public int upgradeLevel;

    public InventoryItemData(ItemSO item, int quantity = 1, int upgradeLevel = 0)
    {
        this.itemSO = item;
        this.quantity = quantity;
        this.upgradeLevel = upgradeLevel;
    }
}
