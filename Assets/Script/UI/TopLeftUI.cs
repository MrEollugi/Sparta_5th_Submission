using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopLeftUI : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI questText;

    public void SetStage(string stageName)
    {
        stageText.text = $"Stage {stageName}";
    }

    public void SetQuest(string questInfo)
    {
        questText.text = questInfo;
    }
}
