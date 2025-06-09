using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPanel : MonoBehaviour
{
    public static ItemDetailPanel Instance { get; private set; }

    [SerializeField] private RectTransform panelRect;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Button equipButton;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private Button sellButton;

    private InventoryItemData currentData;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(InventoryItemData data, Vector3 worldPosition)
    {
        currentData = data;

        iconImage.sprite = data.itemSO.icon;
        nameText.text = data.itemSO.itemName;
        descriptionText.text = data.itemSO.description;

        transform.position = worldPosition + new Vector3(100f, -50f);
        gameObject.SetActive(true);

        SetupActionButtons();
    }

    private void SetupActionButtons()
    {
        equipButton.gameObject.SetActive(currentData.itemSO is EquipmentSO);
        enhanceButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);

        equipButton.onClick.RemoveAllListeners();
        enhanceButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();

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
        else if (currentData.itemSO is EquipmentSO equipment)
        {
            equipButton.gameObject.SetActive(true);
            EquipmentSO equipped = EquipmentManager.Instance.GetEquippedItem(equipment.slotType);
            equipButton.GetComponentInChildren<TMP_Text>().text =
                equipped == equipment ? "Unequip" : "Equip";

            equipButton.onClick.AddListener(() =>
            {
                if (equipped == equipment)
                    EquipmentManager.Instance.Unequip(equipment.slotType);
                else
                    EquipmentManager.Instance.Equip(equipment);

                Hide();
            });
        }
        else
        {
            equipButton.gameObject.SetActive(false);
        }

        enhanceButton.onClick.AddListener(() =>
        {
            Debug.Log("강화 기능 호출");
            Hide();
        });

        sellButton.onClick.AddListener(() =>
        {
            InventoryManager.Instance.RemoveItem(currentData.itemSO, 1);
            Hide();
        });
    }



    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
