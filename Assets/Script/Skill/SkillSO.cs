using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

#region Base Skill ScriptableObject
// Abstract base class for all skill types.
// Includes basic metadata like name, description, and icon.
public abstract class SkillSO : ScriptableObject
{
    [Header("General Info")]
    public string skillName;

    [TextArea] public string description;

    public Sprite icon;
}
#endregion

#region Weapon Active Skill
// Defines an active skill that can be attached to a weapon.
// Contains damage, cooldown, range, and visual/sound effects.
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
#endregion

#region Passive Effect SO
// Base class for all passive effects.
// Handles triggering logic based on cooldown and allows override of the effect.
public abstract class PassiveEffectSO : ScriptableObject
{
    public string effectName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Trigger Settings")]
    public float cooldown = 0f;
    protected float lastActivatedTime = -999f;
    
    // Determines if the effect can be triggered again.
    public virtual bool CanTrigger(PlayerStatus status)
    {
        return Time.time >= lastActivatedTime + cooldown;
    }

    // Called when the effect is triggered. Applies the effect if allowed.
    public virtual void OnEventTriggered(PlayerStatus status)
    {
        if (CanTrigger(status))
        {
            Apply(status);
            lastActivatedTime = Time.time;
        }
    }

    // Applies the actual effect to the player status. Must be implemented by subclasses.
    public abstract void Apply(PlayerStatus status);
}
#endregion

#region Damage Reduction Passive Effect SO
// Passive effect that grants temporary damage reduction when triggered.
[CreateAssetMenu(menuName = "PassiveEffect/DamageReductionOnHit")]
public class DamageReductionPassiveSO : PassiveEffectSO
{
    public float duration = 5f;
    public float reductionPercent = 0.3f;

    public int triggerCount;

    public override void Apply(PlayerStatus status)
    {
        status.ApplyTemporaryDamageReduction(reductionPercent, duration, triggerCount);
    }
}
#endregion

#region Armor Passive Skill
// Represents a container for a list of passive effects tied to an armor item.
[CreateAssetMenu(menuName = "Skill/ArmorPassiveSkill")]
public class ArmorPassiveSkillSO : SkillSO
{
    public List<PassiveEffectSO> passiveEffects;
}
#endregion