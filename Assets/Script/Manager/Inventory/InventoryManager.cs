using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<InventoryItemData> inventoryItems = new();

    public IReadOnlyList<InventoryItemData> Items => inventoryItems;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #region Add / Remove Item

    public void AddItem(ItemSO item, int quantity = 1)
    {
        if (item.isStackable)
        {
            InventoryItemData existing = inventoryItems.FirstOrDefault(x => x.itemSO == item);
            if (existing != null)
            {
                existing.quantity += quantity;
                return;
            }
        }
        inventoryItems.Add(new InventoryItemData(item, quantity));
    }

    public void RemoveItem(ItemSO item, int quantity = 1)
    {
        InventoryItemData target = inventoryItems.FirstOrDefault(x => x.itemSO == item);
        if (target == null) return;

        target.quantity -= quantity;
        if (target.quantity <= 0)
        {
            inventoryItems.Remove(target);
        }
    }

    #endregion

    #region Query

    public List<InventoryItemData> GetItemsByTab(EItemType tabType)
    {
        return inventoryItems.Where(x =>
        {
            if (tabType == EItemType.Consumable && x.itemSO is ConsumableSO) return true;
            if (tabType == EItemType.Equipment && x.itemSO is EquipmentSO) return true;
            return false;
        }).ToList();
    }

    public bool HasItem(ItemSO item, int quantity = 1)
    {
        InventoryItemData found = inventoryItems.FirstOrDefault(x => x.itemSO == item);
        return found != null && found.quantity >= quantity;
    }

    #endregion

}
