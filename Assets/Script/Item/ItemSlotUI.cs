using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void Set(ItemSO item)
    {
        iconImage.sprite = item.icon;
    }
}
