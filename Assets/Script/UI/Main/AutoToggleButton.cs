using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoToggleButton : MonoBehaviour
{
    [SerializeField] private Button autoButton;
    [SerializeField] private TextMeshProUGUI autoText;

    [SerializeField] private PlayerController playerController;

    private bool isAuto = false;

    private void Start()
    {
        autoButton.onClick.AddListener(ToggleAutoMode);
        UpdateUI();
    }

    void ToggleAutoMode()
    {
        isAuto = !isAuto;
        UpdateUI();

        if (playerController != null)
        {
            playerController.SetAutoMode(isAuto);
        }
    }

    void UpdateUI()
    {
        autoText.text = isAuto ? "AUTO ON" : "AUTO OFF";
    }
}
