using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHP = 100;
    public int attack = 20;
    public int defense = 10;

    private int currentHP;
    private float damageReductionMultiplier = 1f;

    [Header("Equipped Passive Skills")]
    public ArmorPassiveSkillSO equippedArmorSkill;
    public AccessoryPassiveSkillSO equippedAccessorySkill;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void OnDamaged(int rawDamage)
    {
        TriggerPassiveEffects();

        int finalDamage = Mathf.CeilToInt(rawDamage * damageReductionMultiplier);
        currentHP = finalDamage;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"[피격] 받은 피해: {finalDamage}, 남은 체력: {currentHP}");
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log($"[회복] {amount} 회복, 현재 체력: {currentHP}");
    }

    public float HPPercent()
    {
        return (float)currentHP / maxHP;
    }

    private void TriggerPassiveEffects()
    {
        if(equippedArmorSkill != null)
        {
            foreach(var effect in equippedArmorSkill.passiveEffects)
            {
                effect.OnEventTriggered(this);
            }
        }

        if(equippedAccessorySkill != null)
        {
            foreach(var effect in equippedAccessorySkill.passiveEffects)
            {
                effect.OnEventTriggered(this);
            }
        }
    }

    public void ApplyTemporaryDamageReduction(float percent, float duration, int hitCount)
    {
        Debug.Log($"[패시브 발동] {percent * 100}% 피해 감소 ({duration}s 동안 {hitCount}회)");
        // TODO: 버프 시스템에서 등록 및 추적
    }
}
