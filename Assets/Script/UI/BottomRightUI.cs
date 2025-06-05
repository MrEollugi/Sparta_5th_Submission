using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class SkillButtonUI
{
    public Button button;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI stackText;
}

public class BottomRightUI : MonoBehaviour
{
    public SkillButtonUI ultimateButton;
    public List<SkillButtonUI> skillButtons;
    public SkillButtonUI potionButton;

    public void SetSkillStack(int index, int current, int max)
    {
        var btn = skillButtons[index];
        btn.stackText.text = $"{current}";
    }

    public void SetSkillKey(int index, string key)
    {
        skillButtons[index].keyText.text = key;
    }
}
