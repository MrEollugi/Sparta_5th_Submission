using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI equippedText;

    private InventoryItemData currentData;
    private InventoryUI inventoryUI;

    public void Set(InventoryItemData data, InventoryUI ui)
    {
        currentData = data;
        inventoryUI = ui;

        iconImage.sprite = data.itemSO.icon;
        quantityText.text = data.itemSO.isStackable ? data.quantity.ToString() : "";

        equippedText.gameObject.SetActive(IsEquipped(data.itemSO));
    }

    public void OnClick()
    {
        ItemDetailPanel.Instance.Show(currentData, transform.position);
    }

    private bool IsEquipped(ItemSO item)
    {
        if (item is ConsumableSO consumable)
            return QuickItemManager.Instance.EquippedQuickItem == consumable;

        if (item is EquipmentSO equipment)
            return EquipmentManager.Instance.GetEquippedItem(equipment.slotType) == equipment;

        return false;
    }
}
