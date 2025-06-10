using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceManager : MonoBehaviour
{
    public static EnhanceManager Instance {  get; private set; }

    public void TryEnhance(InventoryItemData itemData)
    {
        EquipmentSO equipment = itemData.itemSO as EquipmentSO;
        int currentLevel = itemData.upgradeLevel;

        var next = equipment.upgradeLevels.Find(u => u.level == currentLevel + 1);

        if (next == null)
        {
            return;
        }

        if(GoldManager.Instance.CurrentGold < next.cost)
        {
            return;
        }

        GoldManager.Instance.SpendGold(next.cost);
        bool success = Random.value <= next.successRate;

        if (success)
        {
            itemData.upgradeLevel++;
        }

        EnhancePanel.Instance.ShowResult(success, itemData.upgradeLevel);

        EnhancePanel.Instance.Refresh();
    }
}
