using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the logic for enhancing equipment items,
// including success chance and gold consumption.
public class EnhanceManager : MonoBehaviour
{
    #region Singleton
    public static EnhanceManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    // Attempts to enhance the given equipment item.
    // Checks upgrade availability, cost, and calculates success.
    public void TryEnhance(InventoryItemData itemData)
    {
        EquipmentSO equipment = itemData.itemSO as EquipmentSO;
        int currentLevel = itemData.upgradeLevel;

        // Find next upgrade level data
        UpgradeData next = equipment.upgradeLevels.Find(u => u.level == currentLevel + 1);

        if (next == null)
        {
            return;
        }

        if(GoldManager.Instance.CurrentGold < next.cost)
        {
            return;
        }

        // Spend gold and determine enhancement success
        GoldManager.Instance.SpendGold(next.cost);
        bool success = Random.value <= next.successRate;

        if (success)
        {
            itemData.upgradeLevel++;
        }

        // Show result and refresh UI
        EnhancePanel.Instance.ShowResult(success, itemData.upgradeLevel);
        EnhancePanel.Instance.Refresh();
    }
}
