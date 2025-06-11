using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

#region Inventory UI
// Handles the inventory screen UI, including tab switching, item listing,
// equipped item preview, and integration with GoldManager and QuickItemManager.
public class InventoryUI : MonoBehaviour
{
    #region Tab Controls
    [Header("Tab button")]
    [SerializeField] private Button consumableTabButton;
    [SerializeField] private Button equipmentTabButton;

    private EItemType currentTab = EItemType.Consumable;
    #endregion

    #region Slot Prefabs
    [Header("Slot Prefab & List Parents")]
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<InventorySlotUI> spawnedSlots = new();
    #endregion

    #region Detail Panel
    [Header("Detail Panel")]
    [SerializeField] private ItemDetailPanel itemDetailPanel;
    #endregion

    #region Equipped Item Display
    [SerializeField] private GameObject characterSection;

    [Header("Equipments")]
    [SerializeField] private Image equippedWeaponIcon;
    [SerializeField] private Image equippedArmorIcon;
    [SerializeField] private Sprite emptySlotSprite;

    [Header("Consume Item")]
    [SerializeField] private Image equippedConsumableIcon;
    [SerializeField] private TMP_Text equippedConsumableText;
    [SerializeField] private Sprite emptySprite;
    #endregion

    #region Gold UI
    [SerializeField] private TextMeshProUGUI goldAmountText;
    #endregion

    #region Input
    private PlayerInputActions inputActions;
    #endregion

    #region Unity Events
    private void Start()
    {
        // Tab switching
        consumableTabButton.onClick.AddListener(() => SwitchTab(EItemType.Consumable));
        equipmentTabButton.onClick.AddListener(() => SwitchTab(EItemType.Equipment));

        // Equipped item detail handlers
        equippedWeaponIcon.GetComponent<Button>().onClick.AddListener(ShowEquippedWeaponDetail);
        equippedArmorIcon.GetComponent<Button>().onClick.AddListener(ShowEquippedArmorDetail);
        equippedConsumableIcon.GetComponent<Button>().onClick.AddListener(ShowEquippedConsumableDetail);

        // Gold update subscription
        GoldManager.Instance.OnGoldChanged += UpdateGoldUI;
        UpdateGoldUI(GoldManager.Instance.CurrentGold);

        // Quick item update subscription
        QuickItemManager.Instance.OnQuickItemChanged += UpdateQuickItemUI;
        UpdateQuickItemUI(QuickItemManager.Instance.EquippedQuickItem);

        RefreshUI();
    }

    private void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputActions();
        }

        inputActions.UI.Cancel.performed += ctx => OnCancel();
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Cancel.performed -= ctx => OnCancel();
        inputActions.UI.Disable();
    }

    private void OnDestroy()
    {
        if (GoldManager.Instance != null)
            GoldManager.Instance.OnGoldChanged -= UpdateGoldUI;
    }
    #endregion

    #region Tab & Slot Handling
    private void SwitchTab(EItemType tab)
    {
        currentTab = tab;

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
    #endregion

    #region Equipped Items UI
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
    #endregion

    #region UI Updates
    private void UpdateGoldUI(int amount)
    {
        goldAmountText.text = $"{amount:N0} G";
    }

    private void UpdateQuickItemUI(ConsumableSO item)
    {
        equippedConsumableIcon.sprite = item ? item.icon : emptySprite;
        equippedConsumableText.text = item ? item.itemName : "¾øÀ½";
    }
    #endregion

    #region Exit Handling
    private void OnCancel()
    {
        if (gameObject.activeSelf)
        {
            CloseInventory();
        }
    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
#endregion