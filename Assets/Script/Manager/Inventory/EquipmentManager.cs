using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance {  get; private set; }

    [SerializeField] private PlayerStatus playerStatus;

    private Dictionary<EquipmentSlotType, InventoryItemData> equippedItems = new();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach(EquipmentSlotType slot in System.Enum.GetValues(typeof(EquipmentSlotType)))
        {
            equippedItems[slot] = null;
        }
    }

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

    public void Unequip(EquipmentSlotType slot)
    {
        if (equippedItems[slot] == null) return;

        RemoveStats(equippedItems[slot]);
        equippedItems[slot] = null;
    }

    public EquipmentSO GetEquippedItem(EquipmentSlotType slot)
    {
        return equippedItems[slot]?.itemSO as EquipmentSO;
    }

    public InventoryItemData GetEquippedItemData(EquipmentSlotType slot)
    {
        return equippedItems[slot];
    }

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
}