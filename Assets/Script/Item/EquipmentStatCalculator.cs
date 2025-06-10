using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EquipmentStatCalculator
{
    public static int GetAttack(EquipmentSO so, int level)
    {
        if (so == null) return 0;
        return so.bonusAttack + level * so.attackPerLevel;
    }

    public static int GetDefense(EquipmentSO so, int level)
    {
        if (so == null) return 0;
        return so.bonusDefense + level * so.defensePerLevel;
    }
}
