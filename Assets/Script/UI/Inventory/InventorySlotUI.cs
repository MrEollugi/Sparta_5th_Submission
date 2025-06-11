using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Represents a single item slot in the inventory UI.
// Displays item icon, quantity, and whether it's equipped.
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;               // Icon for the item
    [SerializeField] private TextMeshProUGUI quantityText;  // Quantity display (for stackable items)
    [SerializeField] private TextMeshProUGUI equippedText;  // "Equipped" label

    private InventoryItemData currentData;                  // The data associated with this slot
    private InventoryUI inventoryUI;                        // Reference to the inventory UI that owns this slot

    // Initializes the slot with the given item data and parent UI reference.
    public void Set(InventoryItemData data, InventoryUI ui)
    {
        currentData = data;
        inventoryUI = ui;

        iconImage.sprite = data.itemSO.icon;
        quantityText.text = data.itemSO.isStackable ? data.quantity.ToString() : "";

        equippedText.gameObject.SetActive(IsEquipped(data.itemSO));
    }

    // Called when the user clicks this slot.
    // Shows the item detail panel.
    public void OnClick()
    {
        ItemDetailPanel.Instance.Show(currentData, transform.position);
    }

    // Checks if the item is currently equipped (either in quick slot or equipment slot).
    private bool IsEquipped(ItemSO item)
    {
        // Quick item (consumable) check
        if (item is ConsumableSO consumable)
            return QuickItemManager.Instance.EquippedQuickItem == consumable;

        // Equipment slot check
        if (item is EquipmentSO equipment)
            return EquipmentManager.Instance.GetEquippedItem(equipment.slotType) == equipment;

        return false;
    }
}
