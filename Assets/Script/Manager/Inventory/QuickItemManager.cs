using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickItemManager : MonoBehaviour
{
    public static QuickItemManager Instance { get; private set; }

    public ConsumableSO EquippedQuickItem { get; private set; }

    public event System.Action<ConsumableSO> OnQuickItemChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EquipQuickItem(ConsumableSO item)
    {
        EquippedQuickItem = item;
        OnQuickItemChanged?.Invoke(item);
        Debug.Log($"[QuickItem] Equipped: {item.itemName}");
    }

    public void ClearQuickItem()
    {
        EquippedQuickItem = null;
        OnQuickItemChanged?.Invoke(null);
        Debug.Log("[QuickItem] Unequipped");
    }

}
