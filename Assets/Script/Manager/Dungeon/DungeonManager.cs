using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance { get; private set; }

    public DungeonStageSO currentStage;

    private List<GameObject> spawnedEnemies = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartDungeon(DungeonStageSO stage)
    {
        currentStage = stage;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (var data in currentStage.enemyList)
        {
            yield return new WaitForSeconds(data.delay);
            GameObject enemy = Instantiate(data.enemySO.enemyPrefab, data.spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }

        StartCoroutine(CheckClearCondition());
    }

    private IEnumerator CheckClearCondition()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            spawnedEnemies.RemoveAll(e => e == null);

            if (spawnedEnemies.Count == 0)
            {
                OnDungeonCleared();
                break;
            }
        }
    }

    private void OnDungeonCleared()
    {
        Debug.Log("던전 클리어!");

        GoldManager.Instance.AddGold(currentStage.goldReward);
        // CrystalManager.Instance.AddCrystal(currentStage.crystalReward);

        foreach (var item in currentStage.rewardItems)
        {
            InventoryManager.Instance.AddItem(item, 1);
        }

        ResultPanel.Instance.Show(currentStage.goldReward, currentStage.crystalReward, currentStage.rewardItems);
    }
}
