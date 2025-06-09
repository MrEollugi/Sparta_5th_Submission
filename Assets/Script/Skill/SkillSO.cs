using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    [Header("General Info")]
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;
}

[CreateAssetMenu(menuName = "Skill/WeaponActiveSkill")]
public class WeaponActiveSkillSO : SkillSO
{
    [Header ("Stats")]
    public float cooldown = 5f;
    public int power = 10;
    public float range = 1.5f;

    [Header("Effects")]
    public GameObject effectPrefab;
    public AudioClip castSound;
}

public class WeaponPassiveSkillSO : SkillSO
{
}

#region Passive Effect SO
public abstract class PassiveEffectSO : ScriptableObject
{
    public string effectName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Trigger Settings")]
    public float cooldown = 0f;
    protected float lastActivatedTime = -999f;

    public virtual bool CanTrigger(PlayerStatus status)
    {
        return Time.time >= lastActivatedTime + cooldown;
    }

    public virtual void OnEventTriggered(PlayerStatus status)
    {
        if (CanTrigger(status))
        {
            Apply(status);
            lastActivatedTime = Time.time;
        }
    }

    public abstract void Apply(PlayerStatus status);
}
#endregion

#region Damage Reduction Passive Effect SO
[CreateAssetMenu(menuName = "PassiveEffect/DamageReductionOnHit")]
public class DamageReductionPassiveSO : PassiveEffectSO
{
    public float duration = 5f;
    public float reductionPercent = 0.3f;

    public override void Apply(PlayerStatus status)
    {
        status.ApplyTemporaryDamageReduction(reductionPercent, duration, triggerCount);
    }
}
#endregion

#region Armor Passive Skill
[CreateAssetMenu(menuName = "Skill/ArmorPassiveSkill")]
public class ArmorPassiveSkillSO : SkillSO
{
    public List<PassiveEffectSO> passiveEffects;
}
#endregion

[CreateAssetMenu(menuName = "Skill/AccessoryPassiveSkill")]
public class AccessoryPassiveSkillSO : SkillSO
{
    public float critChance;
    public float movementSpeedBonus;
}