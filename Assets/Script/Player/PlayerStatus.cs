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

        Debug.Log($"[�ǰ�] ���� ����: {finalDamage}, ���� ü��: {currentHP}");
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log($"[ȸ��] {amount} ȸ��, ���� ü��: {currentHP}");
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
        Debug.Log($"[�нú� �ߵ�] {percent * 100}% ���� ���� ({duration}s ���� {hitCount}ȸ)");
        // TODO: ���� �ý��ۿ��� ��� �� ����
    }
}
