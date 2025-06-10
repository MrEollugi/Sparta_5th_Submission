using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private TMP_Text stageNameText;
    private Button button;

    public void Set(DungeonStageSO stage, System.Action onClick)
    {
        stageNameText.text = stage.stageName;

        if (button == null)
            button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }
}
