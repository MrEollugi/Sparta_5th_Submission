using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private Transform stageButtonParent;
    [SerializeField] private StageButton stageButtonPrefab;
    [SerializeField] private DungeonStageSO[] allStages;

    private void Start()
    {
        foreach (var stage in allStages)
        {
            var button = Instantiate(stageButtonPrefab, stageButtonParent);
            button.Set(stage, () => OnStageSelected(stage));
        }
    }

    private void OnStageSelected(DungeonStageSO selectedStage)
    {
        Debug.Log($"Selected stage: {selectedStage.stageName}");
        DungeonManager.Instance.StartDungeon(selectedStage);
        gameObject.SetActive(false);
    }
}
