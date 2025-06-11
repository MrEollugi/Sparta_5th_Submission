using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

#region Item Type Enum
// High-level item category: Consumable or Equipment.
public enum EItemType
{
    Consumable,
    Equipment
}
#endregion

#region Equipment Slot Type
// The equipment slot the item can be equipped to.
public enum EquipmentSlotType 
{ 
    Weapon, 
    Armor, 
    Accessory, 
    Pet
}
#endregion

#region Stat Type Enum
// Types of stats that items or effects can modify.
public enum EStatType 
{ 
    HP, 
    MP, 
    Attack, 
    Defense
}
#endregion

#region Base Item ScriptableObject
// Base class for all item types. Shared properties for name, icon, etc.
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public bool isStackable;
}
#endregion

#region Consumable Item
// A consumable item that restores a specific stat (e.g., HP or MP).
[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableSO : ItemSO
{
    public int restoreAmount;
    public EStatType targetStat;
    public float duration;
}
#endregion

#region Upgrade Data
// Represents a single upgrade level for equipment, including cost and success rate.
[System.Serializable]
public class UpgradeData
{
    public int level;
    public int cost;
    public float successRate;
}
#endregion

#region Equipment Base Class
// Abstract base class for all equipment types (weapon, armor, etc.).
// Contains stat bonuses and upgrade level data.
public abstract class EquipmentSO : ItemSO
{
    public EquipmentSlotType slotType;
    public int bonusAttack;
    public int bonusDefense;

    public int attackPerLevel = 1;
    public int defensePerLevel = 2;

    public List<UpgradeData> upgradeLevels;
}
#endregion

#region Weapon Equipment
// Weapon equipment with active skills and passive effects unlocked via upgrades.
[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponSO : EquipmentSO
{
    [Header("Active Skills")]
    public WeaponActiveSkillSO ultimateSkill;
    public List<WeaponActiveSkillSO> availableActiveSkills;
    public List<WeaponActiveSkillSO> equippedActiveSkills;

    [Header("Passive Effects (Unlocked by Upgrade")]
    public List<PassiveEffectSO> passiveStages;

    [Header("Bonus Stats")]
    public float attackSpeed;
}
#endregion

#region Armor Equipment
// Armor equipment with additional HP and damage reduction stats.
[CreateAssetMenu(menuName = "Item/Armor")]
public class ArmorSO : EquipmentSO
{
    public int bonusHP;
    public float damageReduction;

    public int hpPerLevel = 5;
}
#endregion

#region Accessory Equipment
// Accessory equipment with critical chance and damage bonuses.
[CreateAssetMenu(menuName = "Item/Accessory")]
public class AccessorySO : EquipmentSO
{
    public float critChance;
    public float critDamage;
}
#endregion