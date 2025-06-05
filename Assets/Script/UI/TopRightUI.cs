using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopRightUI : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button bagButton;
    [SerializeField] private Button characterButton;

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
        Debug.Log("���� ����");
    }

    void OpenCharacterInfo()
    {
        Debug.Log("ĳ���� ���� ����");
    }
}
