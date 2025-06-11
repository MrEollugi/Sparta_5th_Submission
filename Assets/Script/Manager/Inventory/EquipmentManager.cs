using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Equipment Manager
// Manages equipping and unequipping of equipment items,
// and applies/removes their stat bonuses to the player.
public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (EquipmentSlotType slot in System.Enum.GetValues(typeof(EquipmentSlotType)))
        {
            equippedItems[slot] = null;
        }
    }
    #endregion

    #region References
    [SerializeField] private PlayerStatus playerStatus;
    #endregion

    #region Equipped Items
    // Tracks currently equipped items by slot type.
    private Dictionary<EquipmentSlotType, InventoryItemData> equippedItems = new();
    #endregion

    #region Equip/Unequip Logic
    // Equip an item and apply its stats to the player.
    public void Equip(InventoryItemData itemData)
    {
        var equip = itemData.itemSO as EquipmentSO;
        var slot = equip.slotType;

        if (equippedItems[slot] != null)
        {
            Unequip(slot);
        }

        equippedItems[slot] = itemData;
        ApplyStats(itemData);
    }

    // Unequip item in the specified slot and remove its stats.
    public void Unequip(EquipmentSlotType slot)
    {
        if (equippedItems[slot] == null) return;

        RemoveStats(equippedItems[slot]);
        equippedItems[slot] = null;
    }
    #endregion

    #region Query Methods
    // Get the EquipmentSO of the equipped item in a slot.
    public EquipmentSO GetEquippedItem(EquipmentSlotType slot)
    {
        return equippedItems[slot]?.itemSO as EquipmentSO;
    }

    // Get the full InventoryItemData of the equipped item in a slot.
    public InventoryItemData GetEquippedItemData(EquipmentSlotType slot)
    {
        return equippedItems[slot];
    }
    #endregion

    #region Stat Handling
    // Apply the item's stats to the player's status.
    private void ApplyStats(InventoryItemData itemData)
    {
        var equip = itemData.itemSO as EquipmentSO;
        int level = itemData.upgradeLevel;

        playerStatus.attack += EquipmentStatCalculator.GetAttack(equip, level);
        playerStatus.defense += EquipmentStatCalculator.GetDefense(equip, level);

        if (equip is ArmorSO armor)
        {
            playerStatus.maxHP += armor.bonusHP + level * armor.hpPerLevel;
        }
    }

    // Remove the item's stats from the player's status.
    private void RemoveStats(InventoryItemData itemData)
    {
        var equip = itemData.itemSO as EquipmentSO;
        int level = itemData.upgradeLevel;

        playerStatus.attack -= EquipmentStatCalculator.GetAttack(equip, level);
        playerStatus.defense -= EquipmentStatCalculator.GetDefense(equip, level);

        if (equip is ArmorSO armor)
        {
            playerStatus.maxHP -= armor.bonusHP + level * armor.hpPerLevel;
        }
    }
    #endregion
}
#endregion