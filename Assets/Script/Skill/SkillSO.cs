using UnityEngine;

public enum ESkillType
{
    Weapon,
    Passive
}

public abstract class SkillSO : ScriptableObject
{
    [Header("General Info")]
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;
}

[CreateAssetMenu(menuName = "Skill/ActiveSkill")]
public class ActiveSkillSO : SkillSO
{
    [Header ("Stats")]
    public float cooldown = 5f;
    public int power = 10;
    public float range = 1.5f;

    [Header("Effects")]
    public GameObject effectPrefab;
    public AudioClip castSound;
}

[CreateAssetMenu(menuName = "Skill/ArmorPassiveSkill")]
public class ArmorPassiveSkillSO : SkillSO
{
    public int bonusDefense;
    public float damageReduction;
}

[CreateAssetMenu(menuName = "Skill/AccessoryPassiveSkill")]
public class AccessoryPassiveSkillSO : SkillSO
{
    public float critChance;
    public float movementSpeedBonus;
}