using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Quick Item Manager
// Manages the currently equipped quick-use consumable item (e.g., potion).
// Provides event callback when the quick item changes.
public class QuickItemManager : MonoBehaviour
{
    #region Singleton
    public static QuickItemManager Instance { get; private set; }

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

    #region Properties
    // Currently equipped quick-use consumable item.
    public ConsumableSO EquippedQuickItem { get; private set; }
    // Invoked when the equipped quick item changes (equip or clear).
    public event System.Action<ConsumableSO> OnQuickItemChanged;
    #endregion

    #region Public Methods
    // Equip a new quick-use consumable item.
    public void EquipQuickItem(ConsumableSO item)
    {
        EquippedQuickItem = item;
        OnQuickItemChanged?.Invoke(item);
        Debug.Log($"[QuickItem] Equipped: {item.itemName}");
    }

    // Unequip the currently equipped quick item.
    public void ClearQuickItem()
    {
        EquippedQuickItem = null;
        OnQuickItemChanged?.Invoke(null);
        Debug.Log("[QuickItem] Unequipped");
    }
    #endregion
}
#endregion