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
        Debug.Log("메뉴 열림");
    }

    void OpenInventory()
    {
        Debug.Log("가방 열림");
    }

    void OpenCharacterInfo()
    {
        Debug.Log("캐릭터 정보 열림");
    }
}
