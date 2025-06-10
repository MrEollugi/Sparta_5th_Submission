using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public static ResultPanel Instance {  get; private set; }

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text crystalText;

    [SerializeField] private Transform itemListParent;
    [SerializeField] private ItemSlotUI itemSlotPrefab;

    [SerializeField] private Button confirmButton;

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

    public void Show(int gold, int crystal, List<ItemSO> items)
    {
        titleText.text = "Dungeon Clear!";
        goldText.text = $"+{gold:N0} G";
        crystalText.text = $"+{crystal:N0}";

        foreach (Transform child in itemListParent)
            Destroy(child.gameObject);

        foreach (var item in items)
        {
            var slot = Instantiate(itemSlotPrefab, itemListParent);
            slot.Set(item);
        }

        gameObject.SetActive(true);
    }
}
