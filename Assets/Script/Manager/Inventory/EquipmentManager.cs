using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance {  get; private set; }

    [SerializeField] private PlayerStatus playerStatus;

    private Dictionary<EquipmentSlotType, EquipmentSO> equippedItems = new();

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

    public void Equip(EquipmentSO newEquip)
    {
        EquipmentSlotType slot = newEquip.slotType;

        if(equippedItems[slot] != null)
        {
            Unequip(slot);
        }

        equippedItems[slot] = newEquip;
        ApplyStats(newEquip);
    }

    public void Unequip(EquipmentSlotType slot)
    {
        if (equippedItems[slot] == null) return;

        RemoveStats(equippedItems[slot]);
        equippedItems[slot] = null;
    }

    public EquipmentSO GetEquippedItem(EquipmentSlotType slot)
    {
        return equippedItems[slot];
    }

    private void ApplyStats(EquipmentSO equipment)
    {
        if (equipment == null) return;

        playerStatus.attack += equipment.bonusAttack;
        playerStatus.defense += equipment.bonusDefense;

        if(equipment is ArmorSO armor)
        {
            playerStatus.maxHP += armor.bonusHP;
        }
    }

    private void RemoveStats(EquipmentSO equipment)
    {
        if (equipment == null) return;

        playerStatus.attack -= equipment.bonusAttack;
        playerStatus.defense -= equipment.bonusDefense;

        if (equipment is ArmorSO armor)
        {
            playerStatus.maxHP -= armor.bonusHP;
        }
    }
}