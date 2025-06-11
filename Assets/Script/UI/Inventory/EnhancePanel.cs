using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UI panel for enhancing equipment. Displays current level, cost, and success rate,
// and handles interaction with the EnhanceManager.
public class EnhancePanel : MonoBehaviour
{
    #region Singleton
    public static EnhancePanel Instance { get; private set; }
    #endregion

    #region UI References
    [Header("UI References")]
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text upgradeLevelText;
    [SerializeField] private TMP_Text nextCostText;
    [SerializeField] private TMP_Text successRateText;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private TMP_Text resultText;
    #endregion

    private InventoryItemData currentData;

    #region Unity Events
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        enhanceButton.onClick.AddListener(() =>
        {
            EnhanceManager.Instance.TryEnhance(currentData);
        });
    }
    #endregion

    #region Public Methods
    // Displays the panel and initializes the UI with the given equipment data.
    public void Show(InventoryItemData itemData)
    {
        currentData = itemData;
        resultText.text = "";
        Refresh();
        gameObject.SetActive(true);
    }

    // Refreshes the UI elements based on the current item's upgrade level.
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

    // Displays the result of the enhancement attempt.
    public void ShowResult(bool success, int newLevel)
    {
        if (success)
            resultText.text = $"<color=lime>강화 성공! +{newLevel}</color>";
        else
            resultText.text = $"<color=red>강화 실패...</color>";
    }
    #endregion
}
