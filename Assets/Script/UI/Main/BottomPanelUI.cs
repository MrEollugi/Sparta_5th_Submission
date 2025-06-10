using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Image hpFillImage;
    public Image expFillImage;
    public TextMeshProUGUI hpText;

    public void SetLevel(int level)
    {
        levelText.text = $"Lv. {level}";
    }

    public void SetHP(float current, float max)
    {
        hpFillImage.fillAmount = current / max;
        hpText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }

    public void SetEXP(float current, float max)
    {
        expFillImage.fillAmount = current / max;
    }
}
