using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Tab button")]
    [SerializeField] private Button consumableTabButton;
    [SerializeField] private Button equipmentTabButton;

    [Header("Slot Prefab & List Parents")]
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;

    [Header("Detail Panel")]
    [SerializeField] private ItemDetailPanel itemDetailPanel;

    [SerializeField] private GameObject characterSection;
    [SerializeField] private GameObject equippedConsumableIndicator;

    [Header("Equipments")]
    [SerializeField] private Image equippedWeaponIcon;
    [SerializeField] private Image equippedArmorIcon;
    [SerializeField] private Sprite emptySlotSprite;

    [Header("Consume Item")]
    [SerializeField] private Image equippedConsumableIcon;
    [SerializeField] private TMP_Text equippedConsumableText;
    [SerializeField] private Sprite emptySprite;

    [SerializeField] private TextMeshProUGUI goldAmountText;

    private List<InventorySlotUI> spawnedSlots = new();
    private EItemType currentTab = EItemType.Consumable;

    private void OnDestroy()
    {
        if (GoldManager.Instance != null)
            GoldManager.Instance.OnGoldChanged -= UpdateGoldUI;
    }

    private void Start()
    {
        consumableTabButton.onClick.AddListener(() => SwitchTab(EItemType.Consumable));
        equipmentTabButton.onClick.AddListener(() => SwitchTab(EItemType.Equipment));

        equippedWeaponIcon.GetComponent<Button>().onClick.AddListener(ShowEquippedWeaponDetail);
        equippedArmorIcon.GetComponent<Button>().onClick.AddListener(ShowEquippedArmorDetail);
        equippedConsumableIcon.GetComponent<Button>().onClick.AddListener(ShowEquippedConsumableDetail);

        GoldManager.Instance.OnGoldChanged += UpdateGoldUI;
        UpdateGoldUI(GoldManager.Instance.CurrentGold);

        QuickItemManager.Instance.OnQuickItemChanged += UpdateQuickItemUI;
        UpdateQuickItemUI(QuickItemManager.Instance.EquippedQuickItem);

        RefreshUI();
    }

    private void SwitchTab(EItemType tab)
    {
        currentTab = tab;

        characterSection.SetActive(tab == EItemType.Equipment);
        equippedConsumableIndicator.SetActive(tab == EItemType.Consumable);

        RefreshUI();
    }
    
    private void RefreshUI()
    {
        foreach(InventorySlotUI slot in spawnedSlots)
            Destroy(slot.gameObject);
        spawnedSlots.Clear();

        List<InventoryItemData> items = InventoryManager.Instance.GetItemsByTab(currentTab);
        foreach(InventoryItemData item in items)
        {
            InventorySlotUI newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.Set(item, this);
            spawnedSlots.Add(newSlot);
        }

        itemDetailPanel.Hide();
    }

    public void ShowDetail(InventoryItemData itemData, Vector3 worldPosition)
    {
        itemDetailPanel.Show(itemData, worldPosition);
    }

    private void UpdateGoldUI(int amount)
    {
        goldAmountText.text = $"{amount:N0} G";
    }

    private void UpdateQuickItemUI(ConsumableSO item)
    {
        equippedConsumableIcon.sprite = item ? item.icon : emptySprite;
        equippedConsumableText.text = item ? item.itemName : "¾øÀ½";
    }

    private void UpdateEquippedSlots()
    {
        var weapon = EquipmentManager.Instance.GetEquippedItem(EquipmentSlotType.Weapon);
        equippedWeaponIcon.sprite = weapon ? weapon.icon : emptySlotSprite;

        var armor = EquipmentManager.Instance.GetEquippedItem(EquipmentSlotType.Armor);
        equippedArmorIcon.sprite = armor ? armor.icon : emptySlotSprite;
    }

    private void ShowEquippedWeaponDetail()
    {
        var item = EquipmentManager.Instance.GetEquippedItem(EquipmentSlotType.Weapon);
        if (item != null)
        {
            itemDetailPanel.Show(new InventoryItemData(item), equippedWeaponIcon.transform.position);
        }
    }

    private void ShowEquippedArmorDetail()
    {
        var item = EquipmentManager.Instance.GetEquippedItem(EquipmentSlotType.Armor);
        if (item != null)
        {
            itemDetailPanel.Show(new InventoryItemData(item), equippedArmorIcon.transform.position);
        }
    }

    private void ShowEquippedConsumableDetail()
    {
        var item = QuickItemManager.Instance.EquippedQuickItem;
        if (item != null)
        {
            itemDetailPanel.Show(new InventoryItemData(item), equippedConsumableIcon.transform.position);
        }
    }
}
