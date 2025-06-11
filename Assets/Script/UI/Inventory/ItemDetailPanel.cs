using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#region Item Detail Panel
// Displays detailed information about an inventory item and provides interaction buttons
// such as Equip, Enhance, and Sell.
public class ItemDetailPanel : MonoBehaviour
{
    #region Singleton
    public static ItemDetailPanel Instance { get; private set; }
    #endregion

    #region UI References
    [SerializeField] private RectTransform panelRect;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Button equipButton;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private Button sellButton;
    #endregion

    #region State
    private InventoryItemData currentData;
    #endregion

    #region Unity Events
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    #endregion

    #region Panel Logic
    // Show the detail panel for the given item at the specified world position.
    // data = item data to display.
    // worldPosition = Position to placee the panel near the cursor.
    public void Show(InventoryItemData data, Vector3 worldPosition)
    {
        currentData = data;

        iconImage.sprite = data.itemSO.icon;
        nameText.text = data.itemSO.itemName;
        descriptionText.text = data.itemSO.description;

        transform.position = worldPosition + new Vector3(100f, -50f);   // Offset for readability
        gameObject.SetActive(true);

        SetupActionButtons();
    }

    // Set up functionality of Equip / Enhance / Sell buttons based on item type.
    private void SetupActionButtons()
    {
        equipButton.gameObject.SetActive(currentData.itemSO is EquipmentSO);
        enhanceButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);

        equipButton.onClick.RemoveAllListeners();
        enhanceButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();

        // Quick item (consumable) equip/unequip
        if (currentData.itemSO is ConsumableSO consumable)
        {
            equipButton.gameObject.SetActive(true);
            equipButton.GetComponentInChildren<TMP_Text>().text = QuickItemManager.Instance.EquippedQuickItem == consumable ? "Unequip" : "Equip";

            equipButton.onClick.AddListener(() =>
            {
                if (QuickItemManager.Instance.EquippedQuickItem == consumable)
                {
                    QuickItemManager.Instance.ClearQuickItem();
                }
                else
                {
                    QuickItemManager.Instance.EquipQuickItem(consumable);
                }

                Hide();
            });
        }
        // Equipment equip/unequip
        else if (currentData.itemSO is EquipmentSO equipment)
        {
            equipButton.gameObject.SetActive(true);

            InventoryItemData equipped = EquipmentManager.Instance.GetEquippedItemData(equipment.slotType);
            bool isEquipped = equipped != null &&
                              equipped.itemSO == equipment &&
                              equipped.upgradeLevel == currentData.upgradeLevel;

            equipButton.GetComponentInChildren<TMP_Text>().text = isEquipped ? "Unequip" : "Equip";

            equipButton.onClick.AddListener(() =>
            {
                if (isEquipped)
                    EquipmentManager.Instance.Unequip(equipment.slotType);
                else
                    EquipmentManager.Instance.Equip(currentData);

                Hide();
            });
        }
        else
        {
            equipButton.gameObject.SetActive(false);
        }

        // Enhance button (placeholder)
        enhanceButton.onClick.AddListener(() =>
        {
            Debug.Log("강화 기능 호출");
            Hide();
        });

        // Sell item
        sellButton.onClick.AddListener(() =>
        {
            InventoryManager.Instance.RemoveItem(currentData.itemSO, 1);
            Hide();
        });
    }
    #endregion

    #region Panel Control
    // Hide the panel.
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
#endregion