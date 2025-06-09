using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPanel : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI quantityText;

    [SerializeField] private Button useButton;
    [SerializeField] private Button equipButton;

    [SerializeField] private PlayerStatus playerStatus;

    private InventoryItemData currentItem;

    private void Start()
    {
        useButton.onClick.AddListener(OnUseClicked);
        equipButton.onClick.AddListener(OnEquipClicked);
        Hide();
    }

    public void Show(InventoryItemData itemData)
    {
        currentItem = itemData;

        iconImage.sprite = itemData.itemSO.icon;
        nameText.text = itemData.itemSO.itemName;
        descriptionText.text = itemData .itemSO.description;
        quantityText.text = itemData.itemSO.isStackable ? $"x{itemData.quantity}" : "";

        if(itemData.itemSO is ConsumableSO)
        {
            useButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);
        }
        else if(itemData.itemSO is EquipmentSO equipment)
        {
            useButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnUseClicked()
    {
        if(currentItem.itemSO is ConsumableSO consumable)
        {
            if(consumable.targetStat == EStatType.HP)
            {
                playerStatus.Heal(consumable.restoreAmount);
            }

            InventoryManager.Instance.RemoveItem(currentItem.itemSO, 1);
            Show(currentItem);
        }
    }

    private void OnEquipClicked()
    {
        if (currentItem.itemSO is EquipmentSO equipment)
        {
            var currentEquipped = EquipmentManager.Instance.GetEquippedItem(equipment.slotType);
            if (currentEquipped == equipment)
            {
                EquipmentManager.Instance.Unequip(equipment.slotType);
                Debug.Log($"Unequipped {equipment.itemName}");
            }
            else
            {
                EquipmentManager.Instance.Equip(equipment);
                Debug.Log($"Equipped {equipment.itemName}");
            }

            Show(currentItem);
        }
    }

}
