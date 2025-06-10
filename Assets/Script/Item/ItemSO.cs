using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

#region Item Type Enum
public enum EItemType
{
    Consumable,
    Equipment
}
#endregion

#region Equipment Slot Type

public enum EquipmentSlotType { Weapon, Armor, Accessory, Pet }

#endregion

public enum EStatType { HP, MP, Attack, Defense }

public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public bool isStackable;
}

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableSO : ItemSO
{
    public int restoreAmount;
    public EStatType targetStat;
    public float duration;
}

#region Upgrade Data
[System.Serializable]
public class UpgradeData
{
    public int level;
    public int cost;
    public float successRate;
}
#endregion


public abstract class EquipmentSO : ItemSO
{
    public EquipmentSlotType slotType;
    public int bonusAttack;
    public int bonusDefense;

    public int attackPerLevel = 1;
    public int defensePerLevel = 2;

    public List<UpgradeData> upgradeLevels;
}


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

[CreateAssetMenu(menuName = "Item/Armor")]
public class ArmorSO : EquipmentSO
{
    public int bonusHP;
    public float damageReduction;

    public int hpPerLevel = 5;
}

[CreateAssetMenu(menuName = "Item/Accessory")]
public class AccessorySO : EquipmentSO
{
    public float critChance;
    public float critDamage;
}