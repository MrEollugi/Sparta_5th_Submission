using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Displays the result panel after dungeon completion,
// showing earned gold, crystals, and reward items.
public class ResultPanel : MonoBehaviour
{
    #region Singleton
    public static ResultPanel Instance {  get; private set; }
    #endregion

    #region UI References
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text crystalText;

    [SerializeField] private Transform itemListParent;
    [SerializeField] private ItemSlotUI itemSlotPrefab;

    [SerializeField] private Button confirmButton;
    #endregion

    #region Unity Events
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        confirmButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            // StageSelectUI.Instance.Show();
        });
    }
    #endregion

    #region Public Methods
    // Displays the panel with the given rewards.
    // gold = Amount of gold earned.
    // crystal = Amount of crystals earned.
    // items = List of item rewards.
    public void Show(int gold, int crystal, List<ItemSO> items)
    {
        titleText.text = "Dungeon Clear!";
        goldText.text = $"+{gold:N0} G";
        crystalText.text = $"+{crystal:N0}";

        // Clear previous item slots
        foreach (Transform child in itemListParent)
            Destroy(child.gameObject);

        // Populate new item rewards
        foreach (var item in items)
        {
            var slot = Instantiate(itemSlotPrefab, itemListParent);
            slot.Set(item);
        }

        gameObject.SetActive(true);
    }
    #endregion
}
