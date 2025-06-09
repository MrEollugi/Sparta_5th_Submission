using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopRightUI : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button bagButton;
    [SerializeField] private Button characterButton;
    [SerializeField] private GameObject inventoryPanel;

    private void Start()
    {
        menuButton.onClick.AddListener(OpenMenu);
        bagButton.onClick.AddListener(OpenInventory);
        characterButton.onClick.AddListener(OpenCharacterInfo);
    }

    void OpenMenu()
    {
        Debug.Log("�޴� ����");
    }

    void OpenInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void OpenCharacterInfo()
    {
        Debug.Log("ĳ���� ���� ����");
    }
}
