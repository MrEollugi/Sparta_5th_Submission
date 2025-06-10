using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancePanel : MonoBehaviour
{
    public static EnhancePanel Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text upgradeLevelText;
    [SerializeField] private TMP_Text nextCostText;
    [SerializeField] private TMP_Text successRateText;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private TMP_Text resultText;

    private InventoryItemData currentData;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        enhanceButton.onClick.AddListener(() =>
        {
            EnhanceManager.Instance.TryEnhance(currentData);
        });
    }

    public void Show(InventoryItemData itemData)
    {
        currentData = itemData;
        resultText.text = "";
        Refresh();
        gameObject.SetActive(true);
    }

    public void Refresh()
    {
        EquipmentSO equip = currentData.itemSO as EquipmentSO;
        int level = currentData.upgradeLevel;
        var next = equip.upgradeLevels.Find(u => u.level == level + 1);

        itemNameText.text = equip.itemName;
        upgradeLevelText.text = $"강화 수치: +{level}";

        if (next != null)
        {
            nextCostText.text = $"강화 비용: {next.cost} G";
            successRateText.text = $"성공 확률: {next.successRate * 100f:0}%";
            enhanceButton.interactable = GoldManager.Instance.CurrentGold >= next.cost;
        }
        else
        {
            nextCostText.text = "최대 강화";
            successRateText.text = "-";
            enhanceButton.interactable = false;
        }
    }

    public void ShowResult(bool success, int newLevel)
    {
        if (success)
            resultText.text = $"<color=lime>강화 성공! +{newLevel}</color>";
        else
            resultText.text = $"<color=red>강화 실패...</color>";
    }
}
