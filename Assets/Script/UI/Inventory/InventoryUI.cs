using System.Collections;
using System.Collections.Generic;
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

    private List<InventorySlotUI> spawnedSlots = new();
    private EItemType currentTab = EItemType.Consumable;

    private void Start()
    {
        consumableTabButton.onClick.AddListener(() => SwitchTab(EItemType.Consumable));
        equipmentTabButton.onClick.AddListener(() => SwitchTab(EItemType.Equipment));

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

}
