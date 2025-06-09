using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    private InventoryItemData currentData;
    private InventoryUI inventoryUI;

    public void Set(InventoryItemData data, InventoryUI ui)
    {
        currentData = data;
        inventoryUI = ui;

        iconImage.sprite = data.itemSO.icon;
        quantityText.text = data.itemSO.isStackable ? data.quantity.ToString() : "";
    }

    public void OnClick()
    {
        ItemDetailPanel.Instance.Show(currentData, transform.position);
    }
}
